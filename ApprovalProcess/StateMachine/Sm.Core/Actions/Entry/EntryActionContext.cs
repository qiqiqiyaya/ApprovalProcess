using Sm.Core.Actions.Models;
using Sm.Core.StateMachine;
using Sm.Share.Entities.bases;
using System;

namespace Sm.Core.Actions.Entry
{
	public class EntryActionContext<TState, TTrigger>(
		string stateMachineId,
		IServiceProvider serviceProvider,
		AfterFireContext<TState, TTrigger> afterFire)
		: ActionContext(serviceProvider)
	{
		public string StateMachineId => stateMachineId;

		public AfterFireContext<TState, TTrigger> AfterFire { get; set; } = afterFire;

		public TTrigger Trigger => AfterFire.Trigger;

		public TState CurrentState => AfterFire.CurrentRepresentation.State;

		public TransitionDescription<TState, TTrigger> TransitionDescription => AfterFire.TransitionDescription;

		/// <summary>
		/// 创建者
		/// </summary>
		public CreatorEntity Creator { get; set; }

		/// <summary>
		/// 触发者
		/// </summary>
		public string Firer { get; set; }
	}
}
