namespace Ap.Flow.Behaviours
{
	public class BehaviourContext(Transition transition) : IBehaviourContext
	{
		public Transition Transition { get; } = transition;
	}

	public interface IBehaviourContext
	{
		Transition Transition { get; }
	}
}
