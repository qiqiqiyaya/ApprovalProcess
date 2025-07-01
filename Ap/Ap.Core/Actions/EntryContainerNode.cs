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
            var flow = new Flow()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = context.RootStateSet.Id,
                StateSetId = context.CurrentStateSet.Id,
                StateName = context.State.Name,
                StateId = context.State.Id,
                ExecutorId = context.Executor.Id,
                ParentFlowId = context.Flow.Id
            };

            context.Flow.Nodes.Add(flow);
            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(flow);

            await next(context);
        }
    }
}
