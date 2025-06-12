using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Pipeline
{
    internal class PipelineBuilder<TContext>(string pipelineName) : IPipelineBuilder<TContext>
    {
        private readonly List<TypeMap> _pipes = new();
        private readonly Type _pipeType = typeof(IPipe<TContext>);

        public string PipelineName { get; set; } = pipelineName;

        public IPipelineBuilder<TContext> Use<TPipe>()
            where TPipe : IPipe<TContext>
        {
            _pipes.Add(new TypeMap(typeof(TPipe)));
            return this;
        }

        public IPipelineBuilder<TContext> Use(Type pipeType)
        {
            if (pipeType.GetInterfaces().Any(x => x == _pipeType))
            {
                _pipes.Add(new TypeMap(pipeType));
                return this;
            }

            throw new Exception($"The pipeType is not {_pipeType.FullName}");
        }

        public IPipelineBuilder<TContext> Use(Type pipeType, params object[] parameters)
        {
            if (pipeType.GetInterfaces().Any(x => x == _pipeType))
            {
                _pipes.Add(new TypeMap(pipeType, parameters));
                return this;
            }

            throw new Exception($"The pipeType is not {_pipeType.FullName}");
        }

        public Func<TContext, ValueTask> Build(IServiceProvider serviceProvider)
        {
            List<IPipe<TContext>> list = new List<IPipe<TContext>>();

            for (int index = _pipes.Count - 1; index > -1; index--)
            {
                var map = _pipes[index];
                IPipe<TContext> pipe;

                if (map.Parameters.Length > 0)
                {
                    pipe = (IPipe<TContext>)ActivatorUtilities.CreateInstance(serviceProvider, map.Type, map.Parameters);
                }
                else
                {
                    pipe = (IPipe<TContext>)ActivatorUtilities.CreateInstance(serviceProvider, map.Type);
                }

                list.Add(pipe);
            }

            list.Reverse();
            return Build(list);
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

        private record TypeMap(Type Type, object[] Parameters)
        {
            public TypeMap(Type type) : this(type, [])
            {

            }

            public Type Type { get; set; } = Type;

            public object[] Parameters { get; set; } = Parameters;
        }
    }
}
