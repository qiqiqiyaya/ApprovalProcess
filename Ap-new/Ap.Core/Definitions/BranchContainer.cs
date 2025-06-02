using Ap.Core.Behaviours;
using System;
using System.Linq;

namespace Ap.Core.Definitions
{
    public class BranchContainer(string name, LogicalRelationship relationship, StateSetBase parent)
        : StateSetContainerBase(name, parent)
    {
        public override bool IsEnd => CheckIsEnding();

        public LogicalRelationship Relationship { get; set; } = relationship;

        public override void ExecuteTrigger(StateTrigger trigger)
        {
            if (string.IsNullOrEmpty(trigger.StateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(trigger.StateSetId));
            IStateTrigger set = StateSets[trigger.StateSetId!];
            set.ExecuteTrigger(trigger);

            if (IsEnd)
            {
                // Go directly to the next state
                Parent.ExecuteTrigger(new StateTrigger() { StateSetId = Parent.Id, Trigger = TransitionConst.Direct });
                foreach (var stateSet in StateSets)
                {
                    stateSet.Value.Reset();
                }
            }
        }

        protected override bool CheckIsEnding()
        {
            return Relationship == LogicalRelationship.And ?
                // Ensure all sets are in end state
                StateSets.Values.All(s => s.IsEnd) : StateSets.Values.Any(s => s.IsEnd);
        }

        public override TriggerDictionary GetTrigger()
        {
            TriggerDictionary dic = new TriggerDictionary();
            foreach (var stateSet in StateSets.Values)
            {
                var nodeTriggers = stateSet.GetTrigger();
                foreach (var item in nodeTriggers)
                {
                    item.Value.StateSetId = stateSet.Id;
                    dic.Add(item.Key, item.Value);
                }
            }

            return dic;
        }
    }
}
