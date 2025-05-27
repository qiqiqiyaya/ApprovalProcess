namespace ApNew.Nodes.Behaviours
{
    public class WaitForComplete(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(StateSetBase stateSet)
        {
            return base.ExecuteAsync(stateSet);
        }
    }
}
