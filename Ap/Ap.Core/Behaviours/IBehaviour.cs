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
        ValueTask ExecuteAsync(BaseContext context);

        Transition ToMap();
    }
}
