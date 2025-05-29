namespace Sm.Core.StateMachine
{
    public class ReentryTriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
        : TriggerBehaviour<TState, TTrigger>(trigger, destination);
}
