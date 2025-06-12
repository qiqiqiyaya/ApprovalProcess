using System.Threading.Tasks;
using Ap.Core.Definitions;

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
        ValueTask ExecuteAsync(TriggerContext context);

        Transition ToMap();
    }
}
