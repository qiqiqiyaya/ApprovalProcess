using Ap.Core.Actions.Entry;
using Ap.Core.Actions.Pipeline;
using System.Collections.Generic;

namespace Ap.Core.Actions
{
	public static class ActionContextExtensions
	{
		public static IPipeline<EntryActionContext<TState, TTrigger>> GetEntryPipeline<TState, TTrigger>(this ActionContext context, List<string> entryActionNames)
		{
			if (entryActionNames.Count == 0) return null;

			var container = context.GetRequiredService<ExecutableActionContainer>();
			var maps = container.GetEntryActions(entryActionNames.ToArray());
			return context.GetPipeline<EntryActionContext<TState, TTrigger>>(maps);
		}

		private static IPipeline<TContext> GetPipeline<TContext>(this ActionContext context, List<ExecutableActionMap> maps)
			where TContext : ActionContext
		{
			var provider = context.GetRequiredService<IPipelineProvider>();
			var pipeline = provider.GetPipeline<TContext>(maps);
			return pipeline;
		}

		public static T Propertry<T>(this ActionContext context, string key)
		{
			if (context.Properties.TryGetValue(key, out var value))
			{
				return (T)value;
			}

			return default;
		}
	}
}
