using ApNew.Nodes.Transitions;

namespace ApNew.Nodes
{
    public class StateBase : NodeBase, IState
    {
        public StateBase(string name)
        {
            Name = name;
        }

        public IDictionary<string, INodeTransition> NodeTransitions { get; } =
            new Dictionary<string, INodeTransition>();

        public ValueTask AddTransition(INodeTransition transition)
        {
            Check(transition.Trigger);

            NodeTransitions.Add(transition.Trigger, transition);
            return new ValueTask();
        }

        protected virtual void Check(string trigger)
        {
            if (NodeTransitions.ContainsKey(trigger))
            {
                throw new InvalidOperationException($"State {trigger} already exists in the node transitions.");
            }
        }
    }
}
