namespace ApNew.Nodes.Behaviours
{
    public class JumpOut(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            context.RootSet.CurrentState = destination;
            return new ValueTask();
        }
    }
}
