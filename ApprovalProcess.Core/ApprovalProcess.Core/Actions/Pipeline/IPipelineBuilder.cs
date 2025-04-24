using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions.Pipeline
{
	public interface IPipelineBuilder<TContext>
	{
		internal string PipelineName { get; }

		IPipelineBuilder<TContext> Use<TPipe>() where TPipe : IPipe<TContext>;

		IPipelineBuilder<TContext> Use(Type pipeType);

		internal Func<TContext, ValueTask> Build(IServiceProvider serviceProvider);
	}
}
