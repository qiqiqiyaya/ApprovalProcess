using System;
using System.Threading.Tasks;

namespace Ap.Core.Behaviours
{
    /// <summary>
    /// Reject behaviour is used to reject a state transition.
    /// and return to the start state.
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="destination"></param>
    public class Reject(string trigger, string destination) : BehaviourBase(trigger, destination)
    {
        public override ValueTask ExecuteAsync(BehaviourExecuteContext context)
        {
            var firstState = context.CurrentSet.RootLinkedList.FirstState;

            if (Destination != firstState.Name)
            {
                throw new InvalidOperationException($"Reject behaviour can only be used to return to the first state: {firstState.State}, but was given: {Destination}");
            }

            context.RootSet.CurrentState = Destination;
            return new ValueTask();
        }
    }
}
