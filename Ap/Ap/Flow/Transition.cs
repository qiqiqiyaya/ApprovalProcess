namespace Ap.Flow
{
	/// <summary>
	/// Describes a state transition.
	/// </summary>
	public class Transition
	{
		/// <summary>
		/// Construct a transition.
		/// </summary>
		/// <param name="source">The state transitioned from.</param>
		/// <param name="destination">The state transitioned to.</param>
		/// <param name="trigger">The trigger that caused the transition.</param>
		public Transition(string source, string destination, string trigger)
		{
			Source = source;
			Destination = destination;
			Trigger = trigger;
		}

		/// <summary>
		/// The state transitioned from.
		/// </summary>
		public string Source { get; }

		/// <summary>
		/// The state transitioned to.
		/// </summary>
		public string Destination { get; }

		/// <summary>
		/// The trigger that caused the transition.
		/// </summary>
		public string Trigger { get; }
	}
}
