using System;
using System.Threading.Tasks;

namespace Sm.Core.Actions.Pipeline
{
    public interface IPipelineBuilder<TContext>
    {
        internal string PipelineName { get; }

        IPipelineBuilder<TContext> Use<TPipe>() where TPipe : IPipe<TContext>;

        IPipelineBuilder<TContext> Use(Type pipeType);

        IPipelineBuilder<TContext> Use(Type pipeType, params object[] parameters);

        internal Func<TContext, ValueTask> Build(IServiceProvider serviceProvider);
    }
}
