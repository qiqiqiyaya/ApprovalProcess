using System;
using System.Collections.Generic;
using Ap.Core.Actions.Pipeline;

namespace Ap.Core.Actions.Entry
{
	public class EntryActionContext<TState, TTrigger>(
		IServiceProvider serviceProvider,
		TTrigger trigger,
		TState state,
		TState dtState)
		: ActionContext(serviceProvider)
	{
		public TTrigger Trigger { get; } = trigger;

		public TState State { get; } = state;

		public TState DtState { get; } = dtState;
	}
}
