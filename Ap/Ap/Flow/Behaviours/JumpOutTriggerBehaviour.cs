namespace Ap.Flow.Behaviours
{
	public class JumpOutTriggerBehaviour<TState, TTrigger>(TTrigger trigger,
		TState destination)
		: TriggerBehaviour<TState, TTrigger>(trigger, destination)
	{

	}
}
