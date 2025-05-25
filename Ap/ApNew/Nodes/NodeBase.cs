namespace ApNew.Nodes
{
    public abstract class NodeBase : INode
    {
        public string Id { get; }

        public virtual ValueTask ExecuteAsync()
        {
            return new ValueTask();
        }
    }
}
