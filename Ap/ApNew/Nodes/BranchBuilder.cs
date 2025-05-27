using ApNew.Nodes.Behaviours;
using ApNew.States;

namespace ApNew.Nodes
{
    public class BranchBuilder
    {
        public IDictionary<string, StateSetBuilder> StateSetBuilders { get; } = new Dictionary<string, StateSetBuilder>();

        public LogicalRelationship Relationship { get; }

        public string State { get; }

        private readonly StateSetBuilder _parentBuilder;

        public BranchBuilder(string state, LogicalRelationship relationship, StateSetBuilder parentBuilder)
        {
            Relationship = relationship;
            State = state;
            _parentBuilder = parentBuilder;
        }

        public StateSetBuilder New(string state, string id)
        {
            var provider = new StateSetBuilderProvider();
            var setBuilder = provider.Create(state, id, (result, destination) =>
            {
                var first = _parentBuilder.LinkedList.First!.Value;
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, first.State));
            });

            StateSetBuilders.Add(setBuilder.Id, setBuilder);
            return setBuilder;
        }

        public StateSetBuilder New(string state)
        {
            var provider = new StateSetBuilderProvider();
            var setBuilder = provider.Create("start");

            setBuilder.Then(state);
            StateSetBuilders.Add(setBuilder.Id, setBuilder);
            return setBuilder;
        }

        internal IStateSetContainer Build(IStateSet parent)
        {
            IStateSetContainer container = new BranchContainer(State, Relationship, parent);

            foreach (var builder in StateSetBuilders)
            {
                var linked = builder.Value.LinkedList;
                if (!(linked.Last?.Value is EndState))
                {
                    builder.Value.Complete();
                }

                var set = builder.Value.Build();
                container.StateSets.Add(builder.Key, set);
            }

            return container;
        }
    }
}
