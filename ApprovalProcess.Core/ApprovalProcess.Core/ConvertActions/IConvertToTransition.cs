namespace ApprovalProcess.Core.ConvertActions
{
    /// <summary>
    /// 状态转换器
    /// </summary>
    public interface IConvertToTransition<in TParameter, TState, TTrigger>
    {
        Transition<TState, TTrigger> To(TParameter parameter);
    }
}
