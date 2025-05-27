using ApNew.Nodes.Behaviours;
using ApNew.States;
using System.Reflection.Emit;

namespace ApNew.Nodes
{
    public abstract class StateSetBase : NodeBase, IStateSet
    {
        public string InitialState { get; }

        public string CurrentState { get; set; }

        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

        public bool IsEnd => GetState(CurrentState) is EndState;

        public StateSetBase(IState state)
        {
            Nodes.Add(state.State, state);
            InitialState = state.State;
            CurrentState = state.State;
        }

        public void Configure(IState state)
        {
            if (Nodes.Keys.Contains(state.State)) throw new InvalidOperationException($"State {state.State} already exists in the state set.");

            Nodes.Add(state.State, state);
        }

        public IState GetState(string state)
        {
            if (Nodes.TryGetValue(state, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"State {state} not found in the state set.");
        }

        public virtual void ExecuteTrigger(string trigger)
        {
            IState state = GetState(CurrentState);
            ExitAndEntry(state, new TriggerParameter() { Trigger = trigger });
        }

        public virtual void ExecuteTrigger(TriggerParameter trigger)
        {
            var state = GetState(CurrentState);

            switch (state)
            {
                case IStateSetContainer container:
                    SetContainerHandle(container, trigger);
                    break;
                case StateRepresentation stateRepresentation:
                case StartState startState:
                case StateBase stateBase:
                default:
                    ExitAndEntry(state, trigger);
                    break;
            }

        }

        protected virtual void ExitAndEntry(IState state, TriggerParameter trigger)
        {
            var res = StartStateHandle(state);

            var behaviour = res.NodeTransitions[trigger.Trigger];
            ExitAndEntry(res, behaviour);
            EndStateHandle();
        }

        protected virtual void ExitAndEntry(IState state, INodeBehaviour behaviour)
        {
            state.Exit();
            behaviour.ExecuteAsync(this);
            var nextState = GetState(CurrentState);
            nextState.Entry();
        }

        protected virtual void SetContainerHandle(IStateSetContainer container, TriggerParameter trigger)
        {
            container.ExecuteTrigger(trigger);
        }

        protected virtual IState StartStateHandle(IState state)
        {
            if (state is StartState startState)
            {
                var behaviour = startState.FindNext();
                ExitAndEntry(state, behaviour);
                return GetState(CurrentState); ;
            }

            return state;
        }

        protected virtual void EndStateHandle()
        {
            var current = GetState(CurrentState);
            if (current is EndState endState)
            {
                endState.Exit();
            }
        }
    }
}
