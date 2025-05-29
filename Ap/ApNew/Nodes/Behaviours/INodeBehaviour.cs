using ApNew.Nodes.Core;

namespace ApNew.Nodes.Behaviours
{
    public interface INodeBehaviour
    {
        string Trigger { get; }

        public string Destination { get; }

        /// <summary>
        /// Executing state transition
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ValueTask ExecuteAsync(BehaviourExecuteContext context);
    }
}
