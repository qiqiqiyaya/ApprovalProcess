using ApNew.States;

namespace ApNew.Nodes
{
    public abstract class StateSetBase : NodeBase, IStateSet
    {
        public string InitialState { get; }

        public string CurrentState { get; set; }

        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public LinkedList<IState> LinkedList { get; } = new LinkedList<IState>();

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

        public void ExecuteTrigger(string trigger)
        {
            var state = GetState(CurrentState);
            ExitAndEntry(state, new TriggerParameter() { Trigger = trigger });
        }

        public void ExecuteTrigger(TriggerParameter trigger)
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
            var behaviour = state.NodeTransitions[trigger.Trigger];

            state.Exit();
            behaviour.ExecuteAsync(this);
            var nextState = GetState(CurrentState);
            nextState.Entry();
        }

        protected virtual void SetContainerHandle(IStateSetContainer container, TriggerParameter trigger)
        {
            container.ExecuteTrigger(trigger);
        }
    }
}
