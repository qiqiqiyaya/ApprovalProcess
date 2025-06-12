using Ap.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual void ExecuteTrigger(TriggerContext context)
        {
            var stateSetId = context.StateTrigger.StateSetId;
            if (string.IsNullOrEmpty(stateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(context.StateTrigger.StateSetId));
            IStateTrigger set = StateSets[stateSetId];
            set.ExecuteTrigger(context);

            if (IsEnd)
            {
                // Go directly to the next state
                var stateTrigger = new StateTrigger(ApCoreTriggers.Direct, ToDetail())
                {
                    StateSetId = Parent.Id
                };
                context.StateTrigger = stateTrigger;
                context.CurrentSet = Parent;

                Parent.ExecuteTrigger(context);
                foreach (var stateSet in StateSets)
                {
                    stateSet.Value.Reset();
                }
            }
        }

        public Dictionary<string, IStateSet> StateSets { get; } = new();

        public virtual bool IsEnd => CheckIsEnding();

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

        public override StateTriggerCollection GetTrigger()
        {
            var list = StateSets.Values.SelectMany(s => s.GetTrigger()).ToList();
            return new StateTriggerCollection(list);
        }
    }
}
