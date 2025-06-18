using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Actions
{
    public class TriggerFlowRecord : IExitAction
    {
        public virtual async ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
        {
            await Create(context);
            await next(context);
        }

        protected virtual async ValueTask Create(BaseContext context)
        {
            if (context.Flow is ExecutionFlow executionFlow)
            {
                var record = new FlowRecord(executionFlow);
                await context.GetRequiredService<IFlowService>().AddRecordAsync(record);
            }
        }
    }


}
