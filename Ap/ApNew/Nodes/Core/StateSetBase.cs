using ApNew.Nodes.Behaviours;
using ApNew.Nodes.States;

namespace ApNew.Nodes.Core
{
    public abstract class StateSetBase : StateBase, IStateSet
    {
        public string InitialState { get; }

        public string CurrentState { get; set; }

        public virtual bool IsInitial => CurrentState == InitialState;

        public virtual IState CurrentStateNode => GetState(CurrentState);

        public IDictionary<string, IState> Nodes { get; } = new Dictionary<string, IState>();

        public virtual StateLinkedList LinkedList { get; }

        public StateLinkedList RootLinkedList { get; }

        public bool IsEnd => GetState(CurrentState) is EndState;

        protected StateSetBase(IState state, StateLinkedList rootLinkedList)
            : this(state, rootLinkedList, Guid.NewGuid().ToString("N"))
        {

        }

        protected StateSetBase(IState state, StateLinkedList rootLinkedList, string id)
            : base("StateSetBase_" + id)
        {
            Id = id;

            Nodes.Add(state.State, state);
            InitialState = state.State;
            CurrentState = state.State;

            LinkedList = new StateLinkedList(Nodes.Values);
            RootLinkedList = rootLinkedList;
        }

        public void Configure(IState state)
        {
            Nodes.Add(state.State, state);
            LinkedList.AddLast(state);
        }

        public IState GetState(string state)
        {
            if (Nodes.TryGetValue(state, out var result))
            {
                return result;
            }

            if (RootLinkedList.TryGet(state, out result))
            {
                return result!;
            }

            throw new InvalidOperationException($"State {state} not found in the state set.");
        }

        public override TriggerDictionary GetTrigger()
        {
            var state = GetState(CurrentState);

            // state is never EndState
            // state is StartState ， skip it
            if (state is StartState)
            {
                return LinkedList.FirstState.GetTrigger();
            }

            return state.GetTrigger();
        }

        public virtual void ExecuteTrigger(string trigger)
        {
            ExecuteTrigger(new TriggerParameter() { Trigger = trigger, RootStateSet = this });
        }

        public virtual void ExecuteTrigger(TriggerParameter trigger)
        {
            var state = GetState(CurrentState);

            switch (state)
            {
                case IStateSetContainer container:
                    SetContainerHandle(container, trigger);
                    break;
                case IStateSet set:
                    StateSetHandle(set, trigger);
                    break;
                default:
                    ExitAndEntry(state, trigger);
                    break;
            }
        }

        protected virtual void ExitAndEntry(IState state, TriggerParameter trigger)
        {
            var res = StartStateHandle(state, trigger);

            var behaviour = res.NodeTransitions[trigger.Trigger];
            ExitAndEntry(res, behaviour, trigger);

            EndStateHandle();
        }

        protected virtual void ExitAndEntry(IState state, INodeBehaviour behaviour, TriggerParameter trigger)
        {
            state.Exit();
            behaviour.ExecuteAsync(new BehaviourExecuteContext(trigger.RootStateSet!, this));

            if (IsJumpOut(behaviour.Destination))
            {
                Reset();
                return;
            }

            var nextState = GetState(CurrentState);
            nextState.Entry();
        }

        protected virtual void StateSetHandle(IStateSet set, TriggerParameter trigger)
        {
            if (!set.IsEnd)
            {
                set.ExecuteTrigger(trigger);
            }
            else
            {
                // if it ends, jump out
                ExitAndEntry(set, trigger);
            }
        }

        protected virtual void SetContainerHandle(IStateSetContainer container, TriggerParameter trigger)
        {
            if (!container.IsEnd)
            {
                trigger.RootStateSet ??= this;
                container.ExecuteTrigger(trigger);
            }
            else
            {
                // if it ends, jump out
                ExitAndEntry(container, trigger);
            }
        }

        protected virtual IState StartStateHandle(IState state, TriggerParameter trigger)
        {
            if (state is not StartState startState) return state;
            var behaviour = startState.FindNext();
            ExitAndEntry(state, behaviour, trigger);
            return GetState(CurrentState);
        }

        protected virtual void EndStateHandle()
        {
            if (GetState(CurrentState) is EndState endState) endState.Exit();
        }

        /// <summary>
        /// reset ot initial state
        /// </summary>
        public virtual void Reset()
        {
            CurrentState = InitialState;
        }

        /// <summary>
        /// Jump out this to parent StateSet
        /// </summary>
        protected virtual bool IsJumpOut(string state)
        {
            return !LinkedList.TryGet(state, out _);
        }
    }
}
