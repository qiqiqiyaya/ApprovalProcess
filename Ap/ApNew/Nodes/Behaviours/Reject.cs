namespace ApNew.Nodes.Behaviours
{
    /// <summary>
    /// Reject behaviour is used to reject a state transition.
    /// and return to the start state.
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="destination"></param>
    public class Reject(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(StateSetBase stateSet)
        {
            var linked = stateSet.LinkedList;
            var state = linked.First;
            if (state == null)
            {
                throw new InvalidOperationException("The first State set is empty.");
            }

            if (state.Value.State != destination)
            {
                throw new InvalidOperationException($"The first State set is {state.Value.State}, not equal to {destination}");
            }

            return base.ExecuteAsync(stateSet);
        }
    }
}
