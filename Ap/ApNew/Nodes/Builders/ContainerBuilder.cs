using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Builders
{
    public class ContainerBuilder
    {
        public string Id { get; }

        public IDictionary<string, StateSetBuilder> StateSetBuilderDic { get; } = new Dictionary<string, StateSetBuilder>();

        public LogicalRelationship Relationship { get; }

        public string State { get; }

        public StateLinkedList RootStateLinked { get; }

        public const string ContainerStateNamePrefix = "Container_";

        public ContainerBuilder(LogicalRelationship relationship, StateLinkedList rootStateLinked)
        {
            Id = Guid.NewGuid().ToString("N");

            Relationship = relationship;
            State = ContainerStateNamePrefix + Id;
            RootStateLinked = rootStateLinked;
        }

        public ContainerStateSetBuilder New(string state, string id)
        {
            var provider = new StateSetBuilderProvider(RootStateLinked);

            var containerBuilder = provider.Create<ContainerStateSetBuilder>(
                () => new ContainerStateSetBuilder(state, id, RootStateLinked,
                (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                    result.AddTransition(new Reject(TransitionConst.Reject, first.State));
                }));

            StateSetBuilderDic.Add(containerBuilder.Id, containerBuilder);
            return containerBuilder;
        }
    }
}
