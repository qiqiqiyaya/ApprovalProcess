namespace ApNew.Nodes
{
    public interface INode
    {
        string Id { get; }

        ValueTask ExecuteAsync();
    }
}
