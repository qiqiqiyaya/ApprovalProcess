using System.Collections.Generic;
using Ap.Core.StateMachine;

namespace Ap.Core.Converts.ToStateMachines
{
    public class StateMachineConfiguration<TState, TTrigger>
    {
        public TState InitialState { get; set; }

        public TState CurrentState { get; set; }

        public Dictionary<TState, StateSettings<TState, TTrigger>> StateConfigurations { get; set; } =
            new Dictionary<TState, StateSettings<TState, TTrigger>>();

    }
}
