using Ap.Core.Definitions;

namespace Ap.Core.Builders
{
    public class BranchBuilder : ContainerBuilder
    {
        public LogicalRelationship Relationship { get; }

        public const string BranchStateNamePrefix = "BranchState_";

        public BranchBuilder(LogicalRelationship relationship, StateLinkedList rootStateLinked) : base(rootStateLinked)
        {
            Relationship = relationship;
            State = BranchStateNamePrefix + Id;
        }

        public override IStateSetContainer Build(StateSetBase parent)
        {
            IStateSetContainer container = new BranchContainer(State, Relationship, parent);

            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = (ContainerStateSetBuilder)builder.Value;
                setBuilder.Complete();
                var set = setBuilder.Build();

                container.StateSets.Add(builder.Key, set);
            }

            return container;
        }
    }
}
