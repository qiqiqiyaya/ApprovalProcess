﻿using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateMachines
{
	public interface IStateMachineBuilder<TState, TTrigger>
	{
		IStateMachineBuilder<TState, TTrigger> SetId(string id);

		IStateMachineBuilder<TState, TTrigger> SetInitialState(TState state);

		IStateMachineBuilder<TState, TTrigger> SetCurrentState(TState state);

		IStateMachineBuilder<TState, TTrigger> SetStateConfiguration(StateSettings<TState, TTrigger> settings);

		StateMachine<TState, TTrigger> Build();
	}
}
