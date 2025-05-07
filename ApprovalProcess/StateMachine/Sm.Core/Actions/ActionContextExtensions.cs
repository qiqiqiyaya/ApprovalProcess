using System.Collections.Generic;
using System.Linq;
using Sm.Core.Actions.Entry;
using Sm.Core.Actions.Models;
using Sm.Core.Actions.Pipeline;

namespace Sm.Core.Actions
{
    public static class ActionContextExtensions
    {
        public static IPipeline<EntryActionContext<TState, TTrigger>> GetEntryPipeline<TState, TTrigger>(this ActionContext context, List<StateSettingAction> entryActions)
        {
            if (entryActions.Count == 0) return null;

            var container = context.LazyGetRequiredService<ExecutableActionContainer>();
            var maps = container.GetEntryActions(entryActions.Select(s => s.Name).ToArray());

            maps.ForEach(s =>
            {
                var action = entryActions.First(x => x.Name == s.Action.Name);
                s.Action = action;
            });

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
