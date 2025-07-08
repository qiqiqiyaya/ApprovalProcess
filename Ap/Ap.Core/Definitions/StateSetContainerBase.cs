using Ap.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public abstract class StateSetContainerBase : StateBase, IStateSetContainer
    {
        public const string StateSetContainerIdProperty = "StateSetContainerIdProperty";

        protected readonly StateSetBase Parent;

        protected StateSetContainerBase(string name, StateSetBase parent) : base(name)
        {
            Parent = parent;

            Id = Guid.NewGuid().ToString("N");
        }

        protected StateLinkedList RootLinkedList => Parent.RootLinkedList;

        public Dictionary<string, IStateSet> StateSets { get; } = new();

        public virtual bool IsEnd => CheckIsEnding();

        public virtual IStateSet? CurrentStateSet { get; set; }

        public virtual async ValueTask ExecuteTrigger(TriggerContext context)
        {
            var stateSetId = context.StateTrigger.StateSetId;
            if (string.IsNullOrEmpty(stateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(context.StateTrigger.StateSetId));
            IStateSet set = StateSets[stateSetId!];
            set.ServiceProvider = ServiceProvider;

            await set.ExecuteTrigger(context);

            if (IsEnd)
            {
                // Go directly to the next state
                var stateTrigger = new StateTrigger(ApCoreTriggers.Direct, ToDetail())
                {
                    StateSetId = Parent.Id
                };
                context.StateTrigger = stateTrigger;
                context.CurrentStateSet = Parent;

                await Parent.ExecuteTrigger(context);
                foreach (var stateSet in StateSets)
                {
                    stateSet.Value.Reset();
                }
            }
        }

        /// <summary>
        /// Check if the state is configured in any state set (including children state set).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual bool IsConfigured(string state)
        {
            return StateSets.Any(x => x.Value.LinkedList.Has(state));
        }

        public void Recover(IServiceProvider serviceProvider, List<IState> level)
        {
            if (level.Count == 0) return;

            ServiceProvider = serviceProvider;
            var firstState = level.First();
            level.Remove(firstState);

            var first = StateSets.Values.Single(x => x.Name == firstState.Name);
            CurrentStateSet = first;
            first.Recover(ServiceProvider, level);
        }

        protected virtual bool CheckIsEnding()
        {
            return StateSets.Values.All(s => s.IsEnd);
        }

        public override async ValueTask<StateTriggerCollection> GetTrigger()
        {
            if (IsEnd)
            {
                return new StateTriggerCollection();
            }

            StateTriggerCollection collection = new StateTriggerCollection();
            foreach (var item in StateSets.Values)
            {
                var list = await item.GetTrigger();
                foreach (var value in list)
                {
                    collection.Add(value);
                }
            }

            return collection;
        }

        public override async ValueTask Entry(EntryContext context)
        {
            // create a container for the CurrentStateSet's flow
            await context.ContainerActionRunAsync(StateConfiguration);
        }

        public override async ValueTask Exit(ExitContext context)
        {
            context.CurrentStateSet = Parent;
            context.State = this;
            await base.Exit(context);
        }
    }
}
