namespace ApNew.Nodes
{
    public class BranchContainer : StateBase, IStateSetContainer
    {
        public BranchContainer(string name, LogicalRelationship relationship) : base(name)
        {
            Relationship = relationship;
        }

        public IDictionary<string, IStateSet> StateSets { get; } = new Dictionary<string, IStateSet>();

        public LogicalRelationship Relationship { get; set; }
    }
}
