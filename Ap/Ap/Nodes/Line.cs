namespace Ap.Nodes
{
    /// <summary>
    /// Directed
    /// </summary>
    public class Line : NodeBase, ILine
    {
        public Line(string state) : base(state)
        {

        }

        public IDictionary<string, INode> Nodes { get; } = new Dictionary<string, INode>();

        public LinkedList<INode> LinkedList { get; } = new LinkedList<INode>();
    }
}
