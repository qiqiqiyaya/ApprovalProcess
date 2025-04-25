using System;
using System.Collections.Generic;
using ApprovalProcess.Core.Actions.Pipeline;

namespace ApprovalProcess.Core.Actions
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

		public IPipeline<EntryActionContext<TState, TTrigger>>? GetEntryPipeline(List<string> actionNames)
		{
			if (actionNames.Count == 0) return null;

			var container = GetRequiredService<ExecutableActionContainer>();
			var maps = container.GetEntryActions(actionNames.ToArray());
			return GetPipeline<EntryActionContext<TState, TTrigger>>(maps);
		}

		private IPipeline<TContext> GetPipeline<TContext>(List<ExecutableActionMap> maps)
			where TContext : ActionContext
		{
			var provider = GetRequiredService<IPipelineProvider>();
			var pipeline = provider.GetPipeline<TContext>(maps);
			return pipeline;
		}
	}
}
