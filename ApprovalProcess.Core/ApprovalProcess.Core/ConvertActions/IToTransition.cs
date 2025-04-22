using ApprovalProcess.Core.Entities;

namespace ApprovalProcess.Core.ConvertActions
{
    /// <summary>
    /// 状态转换器
    /// </summary>
    public interface IToTransition<TState, TTrigger>
    {
        Transition<TState, TTrigger> To(TransitionEntity entity);
    }
}
