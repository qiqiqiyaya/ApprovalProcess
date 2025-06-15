using System;
using System.Threading.Tasks;

namespace Ap.Core.Pipeline
{
    internal sealed class Pipeline<TContext> : IPipeline<TContext>
    {
        private readonly Func<TContext, ValueTask> _pipeline;

        internal Pipeline(Func<TContext, ValueTask> pipeline) => _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));

        public ValueTask RunAsync(TContext context) => _pipeline(context);
    }
}
