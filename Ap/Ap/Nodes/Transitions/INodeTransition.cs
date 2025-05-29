namespace Ap.Nodes.Transitions
{
    public interface INodeTransition
    {
        string Trigger { get; }

        public string Destination { get; }


    }
}
