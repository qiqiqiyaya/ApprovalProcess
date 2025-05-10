using System;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateMachines
{
	public class StateMachineBuilder<TState, TTrigger> : IStateMachineBuilder<TState, TTrigger>
	{
		private readonly StateMachineConfiguration<TState, TTrigger> _configuration;

		public StateMachineBuilder()
		{
			_configuration = new StateMachineConfiguration<TState, TTrigger>();
		}

		public IStateMachineBuilder<TState, TTrigger> SetId(string id)
		{
			_configuration.Id = id;
			return this;
		}

		public IStateMachineBuilder<TState, TTrigger> SetInitialState(TState state)
		{
			_configuration.InitialState = state;
			return this;
		}

		public IStateMachineBuilder<TState, TTrigger> SetCurrentState(TState state)
		{
			_configuration.CurrentState = state;
			return this;
		}

		public IStateMachineBuilder<TState, TTrigger> SetStateConfiguration(StateSettings<TState, TTrigger> settings)
		{
			if (_configuration.StateConfigurations.ContainsKey(settings.State))
			{
				throw new Exception("已经存在");
			}

			_configuration.StateConfigurations.Add(settings.State, settings);
			return this;
		}

		public StateMachine<TState, TTrigger> Build()
		{
			var stateMachine = new StateMachine<TState, TTrigger>(_configuration.Id);
			stateMachine.SetCurrentState(_configuration.CurrentState);
			stateMachine.SetInitialState(_configuration.InitialState);
			stateMachine.SetStateConfigurations(_configuration.StateConfigurations);
			return stateMachine;
		}
	}
}
