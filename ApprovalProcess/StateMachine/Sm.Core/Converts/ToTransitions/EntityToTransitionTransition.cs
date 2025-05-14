using Sm.Core.StateMachine;
using Sm.Share.Entities;

namespace Sm.Core.Converts.ToTransitions
{
    public class EntityToTransitionTransition : IConvertToTransition<TransitionEntity, string, string>
    {
        public TriggerBehaviour<string, string> To(TransitionEntity parameter)
        {
            return new TriggerBehaviour<string, string>(parameter.Trigger, parameter.Destination);
        }
    }
}
