using System.Threading.Tasks;

namespace Ap.Core.Behaviours
{
    public class WaitForComplete(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            return base.ExecuteAsync(context);
        }
    }
}
