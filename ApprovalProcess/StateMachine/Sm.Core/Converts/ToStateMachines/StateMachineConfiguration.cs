using System.Collections.Generic;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateMachines
{
	public class StateMachineConfiguration<TState, TTrigger>
	{
		public string Id { get; set; }

		public TState InitialState { get; set; }

		public TState CurrentState { get; set; }

		public Dictionary<TState, StateSettings<TState, TTrigger>> StateConfigurations { get; set; } =
			new Dictionary<TState, StateSettings<TState, TTrigger>>();

	}
}
