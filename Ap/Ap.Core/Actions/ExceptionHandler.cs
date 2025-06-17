using Ap.Core.Definitions;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public class ExceptionHandler : IEntryAction, IExitAction
    {
        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            try
            {
                return next(context);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
        {
            try
            {
                return next(context);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
