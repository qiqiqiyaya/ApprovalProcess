namespace ApNew.Nodes.Behaviours
{
    public abstract class BehaviourBase(string trigger, string destination) : INodeBehaviour
    {
        public string Trigger { get; } = trigger;

        public string Destination { get; } = destination;

        public virtual ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            context.CurrentSet.CurrentState = Destination;
            return new ValueTask();
        }
    }
}
