namespace ApprovalProcess.Core.Converts
{
    /// <summary>
    /// 状态转换器
    /// </summary>
    public interface IConvertTo<in TParameter, TState, TTrigger>
    {
        Transition<TState, TTrigger> To(TParameter parameter);
    }
}
