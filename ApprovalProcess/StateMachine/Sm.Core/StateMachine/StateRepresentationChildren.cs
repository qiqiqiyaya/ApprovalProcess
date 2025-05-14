using System.Collections.Generic;

namespace Sm.Core.StateMachine
{
    public class StateRepresentationChildren<TState, TTrigger>(ParallelRelationship relationship)
        : Dictionary<TState, StateRepresentation<TState, TTrigger>>
    {
        public ParallelRelationship Relationship { get; } = relationship;
    }
}
