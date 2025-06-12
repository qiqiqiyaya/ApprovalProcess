using System.Threading.Tasks;
using Ap.Core.Definitions;

namespace Ap.Core.Behaviours
{
    public class Reentry(string destination) : BehaviourBase(ApCoreTriggers.Reentry, destination)
    {
        public override ValueTask ExecuteAsync(TriggerContext context)
        {
            return new ValueTask();
        }
    }
}
