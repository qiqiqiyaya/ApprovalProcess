using ApprovalProcess.Core.Entities;

namespace ApprovalProcess.Core.ConvertActions
{
    public class StringToTransition : IToTransition<string, string>
    {
        public Transition<string, string> To(TransitionEntity entity)
        {
            return new Transition<string, string>(entity.Trigger, entity.DtState);
        }
    }
}
