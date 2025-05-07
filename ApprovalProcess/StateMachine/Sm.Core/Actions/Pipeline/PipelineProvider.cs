using System;
using System.Collections.Generic;

namespace Sm.Core.Actions.Pipeline
{
    public class PipelineProvider(
        IServiceProvider serviceProvider,
        Dictionary<string, object> pipelineBuilders)
        : IPipelineProvider
    {
        public IPipeline<TContext> GetPipeline<TContext>(string pipeLineName)
        {
            if (pipelineBuilders.TryGetValue(pipeLineName, out var value))
            {
                if (value is IPipelineBuilder<TContext> builder)
                {
                    var func = builder.Build(serviceProvider);

                    IPipeline<TContext> pipeline = new Pipeline<TContext>(func);
                    return pipeline;
                }
            }

            throw new ArgumentException("The specified name does not match pipeline execution context type.", nameof(pipeLineName));
        }

        public IPipeline<TContext> GetPipeline<TContext>(List<ExecutableActionMap> maps, string pipeLineName = "fdsfds")
        {
            IPipelineBuilder<TContext> builder = new PipelineBuilder<TContext>(pipeLineName);

            foreach (var map in maps)
            {
                builder.Use(map.Type, map.Action.Configuration);
            }

            var func = builder.Build(serviceProvider);
            IPipeline<TContext> pipeline = new Pipeline<TContext>(func);
            return pipeline;
        }
    }
}
