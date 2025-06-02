using System.Threading.Tasks;

namespace Ap.Core.Behaviours
{
    public interface IBehaviour
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
