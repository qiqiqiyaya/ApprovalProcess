using System;
using System.Threading.Tasks;

namespace Sm.Core.Actions.Pipeline
{
    public interface IPipe<TContext>
    {
        public abstract ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next);
    }
}
