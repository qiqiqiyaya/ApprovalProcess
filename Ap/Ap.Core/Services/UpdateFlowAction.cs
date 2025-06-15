using Ap.Core.Services.Models;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Definitions;

public class UpdateFlowAction : IExitAction
{
    public ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
    {
        var flow = new Flow();
        flow.Id = context.Flow.Id;
        flow.StateSetId = context.Flow.StateSetId;
        flow.StateName = context.State.Name;
        flow.Id = context.Flow.Id;
        flow.Id = context.Flow.Id;
        flow.Id = context.Flow.Id;
        flow.Id = context.Flow.Id;
    }
}
