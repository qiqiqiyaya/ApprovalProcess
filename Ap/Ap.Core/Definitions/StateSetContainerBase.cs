using Ap.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public abstract class StateSetContainerBase : StateBase, IStateSetContainer
    {
        protected readonly StateSetBase Parent;

        protected StateSetContainerBase(string name, StateSetBase parent) : base(name)
        {
            Parent = parent;

            Id = Guid.NewGuid().ToString("N");
        }

        protected StateLinkedList RootLinkedList => Parent.RootLinkedList;

        public Dictionary<string, IStateSet> StateSets { get; } = new();

        public virtual bool IsEnd => CheckIsEnding();

        public abstract ValueTask InitialEntry(TriggerContext context);

        public abstract ValueTask CompletedExit(TriggerContext context);

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
    }
}
