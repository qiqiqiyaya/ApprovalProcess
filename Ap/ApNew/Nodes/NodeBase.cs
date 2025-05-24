namespace ApNew.Nodes
{
    public abstract class NodeBase : INode
    {
        public string Id { get; }

        public string Name { get; set; }

        public virtual ValueTask ExecuteAsync()
        {
            return new ValueTask();
        }
    }
}
