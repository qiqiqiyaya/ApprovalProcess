using Ap.Core.StateMachine;

namespace Ap.Core.Converts.ToTransitions
{
    /// <summary>
    /// 状态转换器
    /// </summary>
    public interface IConvertToTransition<in TParameter, TState, TTrigger>
    {
        Transition<TState, TTrigger> To(TParameter parameter);
    }
}
