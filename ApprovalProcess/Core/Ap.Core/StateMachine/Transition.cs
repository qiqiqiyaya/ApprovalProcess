namespace Ap.Core.StateMachine
{
    /// <summary>
    /// 状态转换触发器
    /// </summary>
    /// <typeparam name="TState">状态</typeparam>
    /// <typeparam name="TTrigger">触发器</typeparam>
    public class Transition<TState, TTrigger>
    {
        public Transition(TTrigger trigger, TState dt)
        {
            Trigger = trigger;
            DtState = dt;
        }

        public TTrigger Trigger { get; set; }

        /// <summary>
        /// DestinationState
        /// </summary>
        public TState DtState { get; set; }
    }
}
