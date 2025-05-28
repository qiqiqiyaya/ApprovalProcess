using ApNew.Nodes.Core;

namespace ApNew.Nodes.Behaviours
{
    public class WaitForComplete(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            return base.ExecuteAsync(context);
        }
    }
}
