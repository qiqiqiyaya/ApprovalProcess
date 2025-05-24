using ApNew.Nodes.Transitions;

namespace ApNew.Nodes
{
    /// <summary>
    /// node
    /// </summary>
    public interface IState : INode
    {
        /// <summary>
        /// Transition to
        /// </summary>
        IDictionary<string, INodeTransition> NodeTransitions { get; }

        ValueTask AddTransition(INodeTransition transition);
    }
}
