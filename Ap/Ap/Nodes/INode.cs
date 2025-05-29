using Ap.Nodes.Transitions;

namespace Ap.Nodes
{
    /// <summary>
    /// node
    /// </summary>
    public interface INode
    {
        string Id { get; }

        string State { get; }

        IDictionary<string, INodeTransition> NodeTransitions { get; }

        ValueTask AddTransition(INodeTransition transition);

        ValueTask ExecuteAsync();
    }
}
