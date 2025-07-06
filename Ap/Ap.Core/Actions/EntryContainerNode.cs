using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public class EntryContainerNode : IEntryAction
    {
        public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            var flow = context.GetCurrentFlow();

            var container = new FlowContainer()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = context.RootStateSet.Id,
                StateSetId = context.CurrentStateSet.Id,
                StateName = context.State.Name,
                StateId = context.State.Id,
                ExecutorId = context.Executor.Id,
                ParentNodeId = flow.Id,
                IsTriggered = true
            };

            flow.Nodes.Add(container);
            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.RootFlow);

            await next(context);
        }
    }
}
