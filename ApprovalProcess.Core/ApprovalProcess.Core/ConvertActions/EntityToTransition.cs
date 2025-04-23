using ApprovalProcess.Core.Entities;

namespace ApprovalProcess.Core.ConvertActions
{
    public class EntityToTransition : IConvertToTransition<TransitionEntity, string, string>
    {
        public Transition<string, string> To(TransitionEntity parameter)
        {
            return new Transition<string, string>(parameter.Trigger, parameter.DtState);
        }
    }
}
