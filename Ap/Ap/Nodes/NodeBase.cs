using Ap.Nodes.Transitions;

namespace Ap.Nodes
{
    public class NodeBase : INode
    {
        public NodeBase(string state)
        {
            State = state;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string State { get; }

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

        public ValueTask ExecuteAsync()
        {
            return new ValueTask();
        }
    }
}
