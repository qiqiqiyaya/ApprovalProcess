using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes
{
    public class BranchJoinBuilder(StateSetBuilder setBuilder)
    {
        public StateSetBuilder Join(string state)
        {
            if (!setBuilder.Nodes.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state);
                setBuilder.Nodes.Add(state, result);
            }
            setBuilder.LinkedList.AddLast(result);
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
