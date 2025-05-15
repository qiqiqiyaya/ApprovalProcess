namespace Ap.Flow.Behaviours
{
    /// <summary>
    /// 状态转换触发器
    /// </summary>
    /// <typeparam name="TState">状态</typeparam>
    /// <typeparam name="TTrigger">触发器</typeparam>
    public abstract class TriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
    {
        public TTrigger Trigger { get; set; } = trigger;

        /// <summary>
        /// DestinationState
        /// </summary>
        public TState Destination { get; set; } = destination;
    }
}
