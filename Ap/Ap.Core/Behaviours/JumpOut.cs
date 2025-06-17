using System.Threading.Tasks;
using Ap.Core.Definitions;

namespace Ap.Core.Behaviours
{
    public class JumpOut(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(TriggerContext context)
        {
            context.RootStateSet.CurrentState = destination;
            return new ValueTask();
        }
    }
}
