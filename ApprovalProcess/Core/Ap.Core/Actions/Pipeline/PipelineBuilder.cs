using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ap.Core.Actions.Pipeline
{
    internal class PipelineBuilder<TContext>(string pipelineName) : IPipelineBuilder<TContext>
    {
        private readonly List<Type> _pipeTypes = new List<Type>();

        public string PipelineName { get; set; } = pipelineName;

        public IPipelineBuilder<TContext> Use<TPipe>()
            where TPipe : IPipe<TContext>
        {
            _pipeTypes.Add(typeof(TPipe));
            return this;
        }

        public IPipelineBuilder<TContext> Use(Type pipeType)
        {
            var type = typeof(IPipe<TContext>);
            if (pipeType.GetInterfaces().Any(x => x == type))
            {
                _pipeTypes.Add(pipeType);
                return this;
            }

            throw new Exception($"The pipeType is not {type.FullName}");
        }

        public Func<TContext, ValueTask> Build(IServiceProvider serviceProvider)
        {
            List<IPipe<TContext>> list = new List<IPipe<TContext>>();

            for (int index = _pipeTypes.Count - 1; index > -1; index--)
            {
                var type = _pipeTypes[index];
                var pipe = (IPipe<TContext>)ActivatorUtilities.CreateInstance(serviceProvider, type);
                list.Add(pipe);
            }

            list.Reverse();
            return PipelineBuilder<TContext>.Build(list);
        }

        private static Func<TContext, ValueTask> Build(IList<IPipe<TContext>> pipes)
        {
            Func<TContext, ValueTask> nextPipeline = _ => new ValueTask();
            for (int index = pipes.Count - 1; index > -1; index--)
            {
                var pipe = pipes[index];
                var convertedPipe = Convert(pipe);
                nextPipeline = convertedPipe(nextPipeline);
            }
            return nextPipeline;
        }

        private static Func<Func<TContext, ValueTask>, Func<TContext, ValueTask>> Convert(IPipe<TContext> pipe)
        {
            return next => context => InvokeAsync(pipe, context, next);
        }

        private static ValueTask InvokeAsync(IPipe<TContext> pipe, TContext context, Func<TContext, ValueTask> next)
        {
            //if (context is ICancellableContext cancellableContext)
            //{
            //	cancellableContext.CancellationToken.ThrowIfCancellationRequested();
            //}
            //if (context is IAbortableContext abortableContext && abortableContext.IsAborted)
            //{
            //	return new ValueTask();
            //}
            return pipe.InvokeAsync(context, next);
        }
    }
}
