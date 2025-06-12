using Ap.Core.Definitions.Actions;
using System;
using System.Collections.Generic;

namespace Ap.Core.Pipeline
{
    public class PipelineProvider(IServiceProvider serviceProvider) : IPipelineProvider
    {
        public IPipeline<TContext> GetPipeline<TContext>(List<ApAction> actions)
        {
            IPipelineBuilder<TContext> builder = new PipelineBuilder<TContext>(Guid.NewGuid().ToString("N"));

            foreach (var map in actions)
            {
                builder.Use(map.Type, map.Parameters);
            }

            var func = builder.Build(serviceProvider);
            IPipeline<TContext> pipeline = new Pipeline<TContext>(func);
            return pipeline;
        }
    }
}
