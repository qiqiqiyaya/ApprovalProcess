using System.Threading.Tasks;

namespace Ap.Core.Behaviours
{
    public class Reentry(string destination) : BehaviourBase(ApCoreTriggers.Reentry, destination)
    {
        public override ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            context.CurrentSet.Exit();

            context.CurrentSet.Entry();
            return base.ExecuteAsync(context);
        }
    }
}
