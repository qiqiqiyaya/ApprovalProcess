using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
	public class BranchBuilder : ContainerBuilder
	{
		public LogicalRelationship Relationship { get; }

		public const string BranchStateNamePrefix = "BranchState_";

		public BranchBuilder(IStateSetBuilderProvider stateSetBuilderProvider, LogicalRelationship relationship, StateLinkedList rootStateLinked)
			: base(stateSetBuilderProvider, rootStateLinked)
		{
			Relationship = relationship;
			State = BranchStateNamePrefix + Id;
		}

		public override IStateSetContainer Build(StateSetBase parent)
		{
			CheckState();

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
