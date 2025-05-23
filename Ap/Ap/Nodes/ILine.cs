namespace Ap.Nodes
{
    /// <summary>
    /// Single line node
    /// </summary>
    public interface ILine : INode
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        IDictionary<string, INode> Nodes { get; }

        /// <summary>
        /// line
        /// </summary>
        LinkedList<INode> LinkedList { get; }
    }
}
