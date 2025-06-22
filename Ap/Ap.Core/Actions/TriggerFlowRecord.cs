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
            FlowRecord record;
            if (context.CurrentStateSet.IsEnd)
            {
                record = new FlowRecord
                {
                    Id = Guid.NewGuid().ToString("N"),
                    FlowId = context.Flow.Id,
                    RootStateSetId = context.RootStateSet.Id,
                    StateSetId = context.CurrentStateSet.Id,
                    StateName = context.State.Name,
                    StateId = context.StateTrigger.StateDetail.Id,
                    LastExecTrigger = context.StateTrigger.Trigger,
                    ExecutorId = context.Executor.Id,
                    CreateTime = context.Flow.CreateTime,
                    FlowStatus = FlowStatus.End,
                    UpdateTime = DateTime.UtcNow
                };
                record.NextExecutors.Clear();
            }
            else
            {
                record = new FlowRecord(context.Flow);
            }

            await context.GetRequiredService<IFlowManager>().AddRecordAsync(record);
        }
    }


}
