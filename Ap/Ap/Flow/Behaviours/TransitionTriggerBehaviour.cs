namespace Ap.Flow.Behaviours
{
	public class TransitionTriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
		: TriggerBehaviour<TState, TTrigger>(trigger, destination)
	{

	}
}
