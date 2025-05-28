using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
	public class BranchJoinBuilder(StateSetBuilder setBuilder)
	{
		public StateSetBuilder Join(string state)
		{
			if (!setBuilder.StateDictionary.TryGetValue(state, out IState? result))
			{
				result = new StateRepresentation(state);
				setBuilder.StateDictionary.Add(state, result);
			}
			setBuilder.RootStateLinked.AddLast(result);
			setBuilder.AddTransition(state);
			setBuilder.AddTransition = destination =>
			{
				result.AddTransition(new Approve(TransitionConst.Approve, destination));
				result.AddTransition(new Reject(TransitionConst.Reject, destination));
			};

			return setBuilder;
		}
	}
}
