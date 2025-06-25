using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public class BranchContainer(string name, LogicalRelationship relationship, StateSetBase parent)
        : StateSetContainerBase(name, parent)
    {
        public override bool IsEnd => CheckIsEnding();

        //public override ValueTask Entry(EntryContext context)
        //{
        //    var ss = StateSets.Values;
        //}

        public override ValueTask InitialEntry(TriggerContext context)
        {
            throw new NotImplementedException();
        }

        public override ValueTask CompletedExit(TriggerContext context)
        {
            throw new NotImplementedException();
        }

        public LogicalRelationship Relationship { get; set; } = relationship;

        protected override bool CheckIsEnding()
        {
            return Relationship == LogicalRelationship.And ?
                // Ensure all sets are in end state
                StateSets.Values.All(s => s.IsEnd) : StateSets.Values.Any(s => s.IsEnd);
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
