using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Core
{
    public abstract class StateBase : NodeBase, IState
    {
        public StateBase(string state)
        {
            State = state;
        }

        public string State { get; protected set; }

        public IDictionary<string, INodeBehaviour> NodeTransitions { get; } =
            new Dictionary<string, INodeBehaviour>();

        public virtual TriggerDictionary GetTrigger()
        {
            var list = NodeTransitions
                .Select(s => new TriggerResult(s.Key))
                .ToList();

            return new TriggerDictionary(list);
        }

        public void Entry()
        {

        }

        public void Exit()
        {

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
