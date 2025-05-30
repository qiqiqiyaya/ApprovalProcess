using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
	public class BranchBuilder : IBranchBuilder
	{
		public string Id { get; }

		public IDictionary<string, StateSetBuilder> StateSetBuilderDic { get; } = new Dictionary<string, StateSetBuilder>();

		public LogicalRelationship Relationship { get; }

		public string State { get; }

		public StateLinkedList RootStateLinked { get; }

		public const string BranchStateNamePrefix = "BranchState_";

		public BranchBuilder(LogicalRelationship relationship, StateLinkedList rootStateLinked)
		{
			Id = Guid.NewGuid().ToString("N");

			Relationship = relationship;
			State = BranchStateNamePrefix + Id;
			RootStateLinked = rootStateLinked;
		}

		public StateSetBuilder New(string state, string id)
		{
			var provider = new StateSetBuilderProvider(RootStateLinked);
			var setBuilder = provider.Create(state, id, (result, destination) =>
			{
				var first = RootStateLinked.FirstState;
				result.AddTransition(new Approve(TransitionConst.Approve, destination));
				result.AddTransition(new Reject(TransitionConst.Reject, first.State));
			});

			StateSetBuilderDic.Add(setBuilder.Id, setBuilder);
			return setBuilder;
		}

		public virtual bool IsConfigured(string state)
		{
			return StateSetBuilderDic.Any(x => x.Value.IsConfigured(state));
		}

		internal IStateSetContainer Build(StateSetBase parent)
		{
			IStateSetContainer container = new BranchContainer(State, Relationship, parent);

			foreach (var builder in StateSetBuilderDic)
			{
				builder.Value.Complete();
				var set = builder.Value.Build();
				container.StateSets.Add(builder.Key, set);
			}

			return container;
		}
	}
}
