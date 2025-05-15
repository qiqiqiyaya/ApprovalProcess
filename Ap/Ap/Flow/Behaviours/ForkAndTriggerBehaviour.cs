namespace Ap.Flow.Behaviours
{
    public class ForkAndTriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
        : TriggerBehaviour<TState, TTrigger>(trigger, destination)
    {
    }
}
