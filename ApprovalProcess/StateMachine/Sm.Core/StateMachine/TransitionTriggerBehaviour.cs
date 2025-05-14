namespace Sm.Core.StateMachine
{
    public class TransitionTriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
        : TriggerBehaviour<TState, TTrigger>(trigger, destination)
    {

    }
}
