using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sm.Core.Actions.Entry;
using Sm.Core.Actions.Models;

namespace Sm.Core.StateMachine
{
	public class StateMachine<TState, TTrigger>
	{
		public StateMachine(TState initialState)
		{
			InitialState = initialState;
			CurrentState = initialState;

		}

		public string Id { get; }

		/// <summary>
		/// 初始状态
		/// </summary>
		public TState InitialState { get; private set; }

		/// <summary>
		/// 当前状态
		/// </summary>
		public TState CurrentState { get; private set; }

		/// <summary>
		/// 状态表达集
		/// </summary>
		internal IDictionary<TState, StateSettings<TState, TTrigger>> StateConfiguration { get; set; } =
			new Dictionary<TState, StateSettings<TState, TTrigger>>();

		/// <summary>
		/// 配置一个状态内容
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public StateSettings<TState, TTrigger> Configure(TState state)
		{
			if (!StateConfiguration.TryGetValue(state, out StateSettings<TState, TTrigger> result))
			{
				result = new StateSettings<TState, TTrigger>(state);
				StateConfiguration.Add(state, result);
			}

			return result;
		}

		/// <summary>
		/// 触发
		/// </summary>
		public async ValueTask Fire(FireContext<TState, TTrigger> context)
		{
			BeforeFireContext<TState, TTrigger> before = new BeforeFireContext<TState, TTrigger>(context);
			var currentSettings = GetRepresentation(CurrentState);
			before.CurrentSettings = currentSettings;

			currentSettings.TryFindBehaviour(context.Trigger, out var triggerBehaviours);
			var behaviour = triggerBehaviours.First();

			before.NextSettings = GetRepresentation(behaviour.DtState);
			before.DtState = behaviour.DtState;

			TransitionDescription<TState, TTrigger> td = new TransitionDescription<TState, TTrigger>(CurrentState,
				before.DtState,
				context.Trigger);
			before.TransitionDescription = td;

			Exit(before);
			CurrentState = before.DtState;

			AfterFireContext<TState, TTrigger> after = new AfterFireContext<TState, TTrigger>(context);
			after.CurrentSettings = GetRepresentation(CurrentState);
			after.PreviousSettings = currentSettings;
			after.BeforeFire = before;
			after.TransitionDescription = td;

			await Entry(after);
		}

		private void Exit(BeforeFireContext<TState, TTrigger> context)
		{
			var settings = context.CurrentSettings;
			//settings.Exit();
		}

		private async ValueTask Entry(AfterFireContext<TState, TTrigger> context)
		{
			var settings = context.CurrentSettings;
			await settings.Entry(new EntryActionContext<TState, TTrigger>(Id, context.ServiceProvider, context));
		}

		private StateSettings<TState, TTrigger> GetRepresentation(TState state)
		{
			if (StateConfiguration.TryGetValue(state, out StateSettings<TState, TTrigger> result))
			{
				return result;
			}

			throw new Exception($"状态机没有配置状态 {state}");
		}

		internal StateMachine(string id)
		{
			Id = id;
		}

		internal StateMachine<TState, TTrigger> SetInitialState(TState state)
		{
			InitialState = state;
			return this;
		}

		internal StateMachine<TState, TTrigger> SetCurrentState(TState currentState)
		{
			CurrentState = currentState;
			return this;
		}

		internal StateMachine<TState, TTrigger> SetStateConfigurations(Dictionary<TState, StateSettings<TState, TTrigger>> configurations)
		{
			StateConfiguration = configurations;
			return this;
		}
	}
}
