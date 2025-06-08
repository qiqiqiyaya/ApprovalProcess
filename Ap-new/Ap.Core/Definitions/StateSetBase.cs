using Ap.Core.Behaviours;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Ap.Core.Definitions
{
    public abstract class StateSetBase : StateBase, IStateSet
    {
        public string InitialState { get; }

        public string CurrentState { get; set; }

        public virtual bool IsInitial => CurrentState == InitialState;

        public virtual IState CurrentStateNode => GetState(CurrentState);

        public Dictionary<string, IState> StateConfiguration { get; } = new();

        public StateLinkedList LinkedList { get; }

        public StateLinkedList RootLinkedList { get; }

        public virtual bool IsEnd => GetState(CurrentState) is EndState;

        public const string StateSetBaseNamePrefix = "StateSetBase_";

        protected StateSetBase(IState state, StateLinkedList rootLinkedList)
            : this(state, rootLinkedList, Guid.NewGuid().ToString("N"))
        {

        }

        protected StateSetBase(IState state, StateLinkedList rootLinkedList, string id)
            : base(StateSetBaseNamePrefix + id)
        {
            Id = id;

            StateConfiguration.Add(state.Name, state);
            InitialState = state.Name;
            CurrentState = state.Name;

            LinkedList = new(StateConfiguration.Values);
            RootLinkedList = rootLinkedList;
        }

        public void Configure(IState state)
        {
            if (StateConfiguration.ContainsKey(state.Name))
            {
                throw new ApAlreadyExistsException<StateSetDetail>($"State {state.Name} already exists in the state set.", CreateStateSetDetail());
            }

            StateConfiguration.Add(state.Name, state);
            LinkedList.AddLast(state);
        }

        public IState GetState(string state)
        {
            if (StateConfiguration.TryGetValue(state, out var result))
            {
                return result;
            }

            if (RootLinkedList.TryGet(state, out result))
            {
                return result!;
            }

            throw new ApNotFindException<StateSetDetail>($"State {state} not found in the state set.", CreateStateSetDetail());
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

        public virtual void ExecuteTrigger(StateTrigger trigger)
        {
            trigger.RootStateSet ??= this;
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

        protected virtual void ExitAndEntry(IState state, StateTrigger trigger)
        {
            var res = StartStateHandle(state, trigger);

            var behaviour = res.Transitions[trigger.Trigger];
            ExitAndEntry(res, behaviour, trigger);

            EndStateHandle();
        }

        protected virtual void ExitAndEntry(IState state, IBehaviour behaviour, StateTrigger trigger)
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

        protected virtual void StateSetHandle(IStateSet set, StateTrigger trigger)
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

        protected virtual void SetContainerHandle(IStateSetContainer container, StateTrigger trigger)
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

        protected virtual IState StartStateHandle(IState state, StateTrigger trigger)
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



        private StateSetDetail CreateStateSetDetail()
        {
            return new StateSetDetail
            {
                Id = Id,
                Name = Name,
                InitialState = InitialState,
                CurrentState = CurrentState,
                StateConfiguration = new Dictionary<string, IState>(StateConfiguration),
                LinkedList = LinkedList,
                RootLinkedList = RootLinkedList
            };
        }

        private class StateSetDetail
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string InitialState { get; set; }

            public string CurrentState { get; set; }

            public Dictionary<string, IState> StateConfiguration { get; set; }

            public StateLinkedList LinkedList { get; set; }

            public StateLinkedList RootLinkedList { get; set; }
        }
    }
}
