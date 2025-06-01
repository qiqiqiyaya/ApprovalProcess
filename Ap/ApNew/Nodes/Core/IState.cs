using ApNew.Nodes.Behaviours;

namespace ApNew.Nodes.Core
{
    /// <summary>
    /// node
    /// </summary>
    public interface IState : INode
    {
        public string State { get; }

        /// <summary>
        /// Transition to
        /// </summary>
        IDictionary<string, INodeBehaviour> NodeTransitions { get; }

        void AddTransition(INodeBehaviour behaviour);

        void Entry();

        void Exit();

        TriggerDictionary GetTrigger();
    }
}
