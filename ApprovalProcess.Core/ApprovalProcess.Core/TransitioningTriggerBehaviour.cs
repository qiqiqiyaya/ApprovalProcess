namespace ApprovalProcess.Core
{
    /// <summary>
    /// 状态转换触发器
    /// </summary>
    /// <typeparam name="TState">状态</typeparam>
    /// <typeparam name="TTrigger">触发器</typeparam>
    public class TransitioningTriggerBehaviour<TState, TTrigger>
    {

        public TransitioningTriggerBehaviour(TTrigger trigger, TState destination)
        {
            Trigger = trigger;
            DestinationState = destination;
        }

        public TTrigger Trigger { get; set; }

        public TState DestinationState { get; set; }
    }
}
