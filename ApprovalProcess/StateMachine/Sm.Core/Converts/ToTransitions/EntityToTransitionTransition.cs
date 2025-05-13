using Sm.Core.StateMachine;
using Sm.Share.Entities;

namespace Sm.Core.Converts.ToTransitions
{
    public class EntityToTransitionTransition : IConvertToTransition<TransitionEntity, string, string>
    {
        public Transition<string, string> To(TransitionEntity parameter)
        {
            return new Transition<string, string>(parameter.Trigger, parameter.Destination);
        }
    }
}
