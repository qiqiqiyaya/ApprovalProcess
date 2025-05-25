namespace ApNew.Nodes.Behaviours
{
    public abstract class BehaviourBase(string trigger, string destination) : INodeBehaviour
    {
        public string Trigger { get; } = trigger;

        public string Destination { get; } = destination;
    }
}
