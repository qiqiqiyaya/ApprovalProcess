using Ap.Core.Behaviours;
using Ap.Core.Configurations;
using Ap.Core.Definitions.States;
using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Recover(IServiceProvider serviceProvider, string stateName)
        {
            ServiceProvider = serviceProvider;

            var level = RootLinkedList.GetStateLevel(stateName);
            var first = level.First();
            CurrentState = first.Name;
            level.Remove(first);

            Recover(CurrentStateNode, level);
        }

        public void Recover(IServiceProvider serviceProvider, string stateName, List<IState> level)
        {
            ServiceProvider = serviceProvider;
            CurrentState = stateName;
            Recover(CurrentStateNode, level);
        }

        private void Recover(IState state, List<IState> level)
        {
            if (level.Count == 0) return;
            var firstState = level.First();
            level.Remove(firstState);
            switch (state)
            {
                case IStateSet set:
                    set.Recover(ServiceProvider, firstState.Name, level);
                    break;
                case IStateSetContainer container:
                    container.StateSets[firstState.Name].Recover(ServiceProvider, firstState.Name, level);
                    break;
            }
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

        public override async ValueTask<StateTriggerCollection> GetTrigger()
        {
            if (IsEnd) return new StateTriggerCollection();
            var state = GetState(CurrentState);
            state.ServiceProvider = ServiceProvider;

            // state is never EndState
            // state is StartState ， skip it
            var collection = new StateTriggerCollection();

            switch (state)
            {
                case IStateSetContainer:
                    collection = await state.GetTrigger();
                    break;
                case StartState:
                    collection = await LinkedList.FirstState.GetTrigger();

                    foreach (var stateTrigger in collection)
                    {
                        stateTrigger.StateSetId = Id;
                    }
                    break;
            }

            return collection;
        }

        public virtual async ValueTask ExecuteTrigger(TriggerContext context)
        {
            context.RootStateSet ??= this;
            context.StateSetConfiguration = StateSetConfiguration;
            context.ServiceProvider = ServiceProvider;
            context.TriggeredTime = DateTime.UtcNow;

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
            //var res = await StartStateHandle(state, context);

            var behaviour = state.Transitions[context.StateTrigger.Trigger];
            await ExitAndEntry(state, behaviour, context);

            //await EndStateHandle(context);
        }

        protected virtual async ValueTask ExitAndEntry(IState state, IBehaviour behaviour, TriggerContext context)
        {
            context.CurrentStateSet = this;
            context.State = state;

            await state.Exit(context.CreateExitContext());
            await behaviour.ExecuteAsync(context);

            if (IsJumpOut(behaviour.Destination))
            {
                Reset();
            }

            var nextState = GetState(CurrentState);
            context.State = nextState;
            nextState.ServiceProvider = ServiceProvider;
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

        public virtual async ValueTask InitialEntry(TriggerContext context)
        {
            context.RootStateSet ??= this;
            context.StateSetConfiguration = StateSetConfiguration;
            context.ServiceProvider = ServiceProvider;
            context.TriggeredTime = DateTime.UtcNow;

            var state = (GetState(CurrentState) as StartState)!;
            var behaviour = state.GetBehaviour();
            await ExitAndEntry(state, behaviour, context);
        }

        public virtual ValueTask CompletedExit(TriggerContext context)
        {
            return new ValueTask();
        }

        /// <summary>
        /// reset ot initial state
        /// </summary>
        public virtual void Reset()
        {
            if (!IsEnd)
            {
                throw new InvalidOperationException("Cannot reset a state set that is not in an end state.");
            }
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
