using ApprovalProcess.Core.Entities;

namespace ApprovalProcess.Core.Converts.ToTransitions
{
    public class EntityToTransitionTransition : IConvertToTransition<TransitionEntity, string, string>
    {
        public Transition<string, string> To(TransitionEntity parameter)
        {
            return new Transition<string, string>(parameter.Trigger, parameter.DtState);
        }
    }
}
