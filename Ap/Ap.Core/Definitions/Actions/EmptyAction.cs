using System;
using System.Threading.Tasks;

namespace Ap.Core.Definitions.Actions
{
    public class EmptyAction : IEntryAction, IExitAction
    {
        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            return next(context);
        }

        public ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
        {
            return next(context);
        }
    }
}
