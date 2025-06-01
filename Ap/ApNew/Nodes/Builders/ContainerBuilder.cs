using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
    public class ContainerBuilder
    {
        public string Id { get; }

        public IDictionary<string, StateSetBuilder> StateSetBuilderDic { get; } = new Dictionary<string, StateSetBuilder>();

        public string State { get; }

        public StateLinkedList RootStateLinked { get; }

        public const string ContainerStateNamePrefix = "Container_";

        public ContainerBuilder(StateLinkedList rootStateLinked)
        {
            Id = Guid.NewGuid().ToString("N");

            State = ContainerStateNamePrefix + Id;
            RootStateLinked = rootStateLinked;
        }

        public ContainerStateSetBuilder New(string state)
        {
            var provider = new StateSetBuilderProvider(RootStateLinked);

            var containerBuilder = provider.Create<ContainerStateSetBuilder>(
                () => new ContainerStateSetBuilder(state, Guid.NewGuid().ToString("N"), RootStateLinked,
                (result, destination) =>
                {
                    var first = RootStateLinked.FirstState;
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                    result.AddTransition(new Reject(TransitionConst.Reject, first.State));
                }));

            StateSetBuilderDic.Add(containerBuilder.Id, containerBuilder);
            return containerBuilder;
        }

        internal IStateSetContainer Build(StateSetBase parent)
        {
            StateSetContainer container = new StateSetContainer(State, parent);

            foreach (var builder in StateSetBuilderDic)
            {
                var setBuilder = builder.Value;
                setBuilder.Complete();
                var set = setBuilder.Build();

                container.StateSets.Add(builder.Key, set);
            }

            return container;
        }
    }
}
