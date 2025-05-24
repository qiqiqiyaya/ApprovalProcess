namespace ApNew.Nodes.Transitions
{
    public abstract class TransitionBase(string trigger, string destination) : INodeTransition
    {
        public string Trigger { get; } = trigger;

        public string Destination { get; } = destination;
    }
}
