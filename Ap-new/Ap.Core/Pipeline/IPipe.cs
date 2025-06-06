using System;
using System.Threading.Tasks;

namespace Ap.Core.Pipeline
{
    public interface IPipe<TContext>
    {
        ValueTask InvokeAsync(TContext context, Func<TContext, ValueTask> next);
    }
}
