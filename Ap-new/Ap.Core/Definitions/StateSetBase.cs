using Ap.Core.Behaviours;
using Ap.Core.Configurations;
using Ap.Core.Definitions.States;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public abstract class StateSetBase : StateBase, IStateSet
    {
        public const string StateSetBaseNamePrefix = "StateSetBase_";

        public string InitialState { get; }

        public string CurrentState { get; set; }

        public virtual bool IsInitial => CurrentState == InitialState;

        public virtual IState CurrentStateNode => GetState(CurrentState);

        public Dictionary<string, IState> StateDictionary { get; } = new();

        public StateLinkedList LinkedList { get; }

        public StateLinkedList RootLinkedList { get; }

        public virtual bool IsEnd => GetState(CurrentState) is EndState;

        public virtual StateSetConfiguration StateSetConfiguration { get; } = new();

        protected StateSetBase(IState state, StateLinkedList rootLinkedList)
            : this(state, rootLinkedList, Guid.NewGuid().ToString("N"))
        {

        }

        protected StateSetBase(IState state, StateLinkedList rootLinkedList, string id)
            : base(StateSetBaseNamePrefix + id)
        {
            Id = id;

            StateDictionary.Add(state.Name, state);
            InitialState = state.Name;
            CurrentState = state.Name;

            LinkedList = new(StateDictionary.Values);
            RootLinkedList = rootLinkedList;
        }

        public void AddState(IState state)
        {
            if (StateDictionary.ContainsKey(state.Name))
            {
                throw new ApAlreadyExistsException<StateSetDetail>($"State {state.Name} already exists in the state set.", CreateStateSetDetail());
            }

            StateDictionary.Add(state.Name, state);
            LinkedList.AddLast(state);
        }

        public void Recover(string stateName)
        {
            CurrentState = stateName;
        }

        public IState GetState(string state)
        {
            if (StateDictionary.TryGetValue(state, out var result))
            {
                return result;
            }

            if (RootLinkedList.TryGet(state, out result))
            {
                return result!;
            }

            throw new ApNotFindException<StateSetDetail>($"State {state} not found in the state set.", CreateStateSetDetail());
        }

        public override StateTriggerCollection GetTrigger()
        {
            var state = GetState(CurrentState);

            // state is never EndState
            // state is StartState ， skip it
            var collection = state is StartState ? LinkedList.FirstState.GetTrigger() : state.GetTrigger();

            foreach (var stateTrigger in collection)
            {
                stateTrigger.StateSetId = Id;
            }

            return collection;
        }

        public virtual async ValueTask ExecuteTrigger(TriggerContext context)
        {
            context.RootSet = this;
            context.RootSetConfiguration = StateSetConfiguration;
            var state = GetState(CurrentState);

            switch (state)
            {
                case IStateSetContainer container:
                    await SetContainerHandle(container, context);
                    break;
                case IStateSet set:
                    await StateSetHandle(set, context);
                    break;
                default:
                    await ExitAndEntry(state, context);
                    break;
            }
        }

        protected virtual async ValueTask ExitAndEntry(IState state, TriggerContext context)
        {
            var res = await StartStateHandle(state, context);

            var behaviour = res.Transitions[context.StateTrigger.Trigger];
            await ExitAndEntry(res, behaviour, context);

            EndStateHandle();
        }

        protected virtual async ValueTask ExitAndEntry(IState state, IBehaviour behaviour, TriggerContext context)
        {
            state.Exit();
            context.CurrentSet = this;
            await behaviour.ExecuteAsync(context);

            if (IsJumpOut(behaviour.Destination))
            {
                Reset();
            }

            var nextState = GetState(CurrentState);
            await nextState.Entry(context.CreateEntryContext());
        }

        protected virtual async ValueTask StateSetHandle(IStateSet set, TriggerContext context)
        {
            if (!set.IsEnd)
            {
                await set.ExecuteTrigger(context);
            }
            else
            {
                // if it ends, jump out
                await ExitAndEntry(set, context);
            }
        }

        protected virtual async ValueTask SetContainerHandle(IStateSetContainer container, TriggerContext context)
        {
            if (!container.IsEnd)
            {
                await container.ExecuteTrigger(context);
            }
            else
            {
                // if it ends, jump out
                await ExitAndEntry(container, context);
            }
        }

        protected virtual async ValueTask<IState> StartStateHandle(IState state, TriggerContext context)
        {
            if (state is not StartState startState) return state;
            context.CurrentSet = this;
            await startState.Entry(context.CreateEntryContext());

            var behaviour = startState.GetBehaviour();
            await ExitAndEntry(state, behaviour, context);
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
                StateConfiguration = new Dictionary<string, IState>(StateDictionary),
                LinkedList = LinkedList,
                RootLinkedList = RootLinkedList
            };
        }
    }
}
