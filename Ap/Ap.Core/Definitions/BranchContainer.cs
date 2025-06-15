using Ap.Core.Behaviours;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public class BranchContainer(string name, LogicalRelationship relationship, StateSetBase parent)
        : StateSetContainerBase(name, parent)
    {
        public override bool IsEnd => CheckIsEnding();

        public LogicalRelationship Relationship { get; set; } = relationship;

        public override async ValueTask ExecuteTrigger(TriggerContext context)
        {
            var stateSetId = context.StateTrigger.StateSetId;
            if (string.IsNullOrEmpty(stateSetId)) throw new ArgumentException("StateSetId cannot be null or empty.", nameof(context.StateTrigger.StateSetId));
            IStateTrigger set = StateSets[stateSetId!];
            await set.ExecuteTrigger(context);

            if (IsEnd)
            {
                // Go directly to the next state
                var stateTrigger = new StateTrigger(ApCoreTriggers.Direct, ToDetail())
                {
                    StateSetId = Parent.Id
                };
                context.StateTrigger = stateTrigger;
                context.CurrentSet = Parent;

                await Parent.ExecuteTrigger(context);
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

        public override StateTriggerCollection GetTrigger()
        {
            if (IsEnd)
            {
                return new StateTriggerCollection();
            }

            var list = StateSets.Values
                .Where(x => !x.IsEnd)
                .SelectMany(s => s.GetTrigger())
                .ToList();

            return new StateTriggerCollection(list);
        }
    }
}
