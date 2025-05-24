using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApNew.Nodes
{
    public class BranchBuilder
    {
        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public LogicalRelationship Relationship { get; set; }

        public BranchBuilder(LogicalRelationship relationship)
        {
            Relationship = relationship;
        }


    }
}
