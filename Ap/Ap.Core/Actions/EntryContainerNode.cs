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
            var container = new FlowContainer()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = context.RootStateSet.Id,
                StateSetId = context.CurrentStateSet.Id,
                StateName = context.State.Name,
                StateId = context.State.Id,
                ExecutorId = context.Executor.Id,
                ParentFlowId = context.Flow.Id,
                IsTriggered = true
            };

            context.Flow.Nodes.Add(container);
            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.Flow);

            await next(context);
        }
    }
}
