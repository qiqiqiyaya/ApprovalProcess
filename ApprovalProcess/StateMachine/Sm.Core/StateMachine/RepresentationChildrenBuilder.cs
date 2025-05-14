using System.Collections.Generic;

namespace Sm.Core.StateMachine
{
    public class RepresentationChildrenBuilder<TState, TTrigger>(ParallelRelationship relationship)
    {
        private readonly IDictionary<TState, StateRepresentation<TState, TTrigger>> _stateConfiguration = new Dictionary<TState, StateRepresentation<TState, TTrigger>>();

        private readonly ParallelRelationship _relationship = relationship;

        public StateRepresentation<TState, TTrigger> Configure(TState state)
        {
            if (!_stateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                _stateConfiguration.Add(state, result);
            }

            return result;
        }

        internal StateRepresentationChildren<TState, TTrigger> Build()
        {
            var children = new StateRepresentationChildren<TState, TTrigger>(_relationship);
            return children;
        }
    }
}
