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

        public virtual void ExecuteTrigger(StateTrigger trigger)
        {
            if (string.IsNullOrEmpty(trigger.StateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(trigger.StateSetId));
            IStateTrigger set = StateSets[trigger.StateSetId!];
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                Parent.ExecuteTrigger(new StateTrigger() { StateSetId = Parent.Id, Trigger = ApCoreTriggers.Direct });
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
            return StateSets.Values.SelectMany(s => s.GetTrigger()).ToList();
        }
    }
}
