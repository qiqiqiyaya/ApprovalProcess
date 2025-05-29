namespace Ap.Flow.Behaviours
{
	public interface ITriggerBehaviour
	{
		public string Trigger { get; }

		/// <summary>
		/// DestinationState
		/// </summary>
		public string Destination { get; }

		ValueTask InvokeAsync(IBehaviourContext context);
	}
}
