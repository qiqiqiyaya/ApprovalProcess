using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public class ExitFlowNode : IExitAction
    {
        public async ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
        {
            var currentFlow = context.GetCurrentFlow();
            currentFlow.UpdateTime = DateTime.UtcNow;
            currentFlow.ExecutorId = context.Executor.Id;
            currentFlow.IsTriggered = false;
            currentFlow.FlowStatus = FlowStatus.Completed;

            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.RootFlow);
        }
    }
}
