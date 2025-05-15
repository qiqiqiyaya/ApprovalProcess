namespace Ap.Flow
{
	public class StateRepresentationChildren<TState, TTrigger>(ForkRelationship relationship)
		: Dictionary<TState, StateRepresentation<TState, TTrigger>>
	{
		public ForkRelationship Relationship { get; } = relationship;
	}
}
