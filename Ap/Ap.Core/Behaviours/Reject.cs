using System;
using System.Threading.Tasks;
using Ap.Core.Definitions;

namespace Ap.Core.Behaviours
{
    /// <summary>
    /// Reject behaviour is used to reject a state transition.
    /// and return to the start state.
    /// </summary>
    /// <param name="destination"></param>
    public class Reject(string destination) : BehaviourBase(ApCoreTriggers.Reject, destination)
    {
        public override ValueTask ExecuteAsync(TriggerContext context)
        {
            var firstState = context.CurrentStateSet.RootLinkedList.FirstState;

            if (Destination != firstState.Name)
            {
                throw new InvalidOperationException($"Reject behaviour can only be used to return to the first state: {firstState.Name}, but was given: {Destination}");
            }

            context.RootStateSet.CurrentState = Destination;
            return new ValueTask();
        }
    }
}
