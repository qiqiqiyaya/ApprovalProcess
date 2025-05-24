namespace ApNew.Nodes
{
    public interface INode
    {
        string Id { get; }

        string Name { get; }

        ValueTask ExecuteAsync();
    }
}
