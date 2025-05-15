namespace Ap.Flow.Behaviours
{
	public class FinishTriggerBehaviour<TState, TTrigger>(TTrigger trigger)
		: TriggerBehaviour<TState, TTrigger>(trigger, default(TState))
	{
	}
}
