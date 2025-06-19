using Ap.Core.Definitions;
using Ap.Core.Exceptions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions;

/// <summary>
/// Updates the flow state and approvers.
/// </summary>
public class ModifyFlow : IEntryAction
{
    public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
    {
        var flow = new Flow();
        flow.Id = context.Flow.Id;
        flow.RootStateSetId = context.RootStateSet.Id;
        flow.CurrentStateSetId = context.CurrentStateSet.Id;
        flow.StateName = context.State.Name;
        flow.StateId = context.StateTrigger.StateDetail.Id;
        flow.StateTrigger = context.StateTrigger.Trigger;
        flow.ExecutorId = context.Executor.Id;
        flow.CreateTime = DateTime.UtcNow;
        flow.NextExecutors.Clear();

        context.Flow = flow;

        await next(context);

        if (context.NextApproverList.Count == 0)
        {
            throw new ApException("No approvers assigned for the flow.");
        }

        flow.NextExecutors = context.NextApproverList.ConvertAll(s =>
        {
            var np = new NextExecutor
            {
                Id = Guid.NewGuid().ToString("N"),
                ObjectId = s,
                FlowId = flow.Id,
                CreateTime = DateTime.UtcNow
            };
            return np;
        });

        await context.GetRequiredService<IFlowService>().UpdateAsync(flow);
    }
}
