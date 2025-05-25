using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes
{
    public class StateBase : NodeBase, IState
    {
        public StateBase(string state)
        {
            State = state;
        }

        public string State { get; }

        public IDictionary<string, INodeBehaviour> NodeTransitions { get; } =
            new Dictionary<string, INodeBehaviour>();

        public void Entry()
        {

        }

        public void Exit()
        {
            //var behaviour = NodeTransitions[trigger];

            //return behaviour.Destination;
        }



        public void AddTransition(INodeBehaviour behaviour)
        {
            Check(behaviour.Trigger);

            NodeTransitions.Add(behaviour.Trigger, behaviour);
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
