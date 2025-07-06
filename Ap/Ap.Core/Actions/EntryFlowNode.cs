using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public class EntryFlowNode : IEntryAction
    {
        public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            var container = GetContainer(context.RootFlow);
            if (container != null)
            {
                var flow = new Flow()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    RootStateSetId = context.RootStateSet.Id,
                    StateSetId = context.CurrentStateSet.Id,
                    StateName = context.CurrentStateSet.Name,
                    StateId = context.State.Id,
                    ExecutorId = context.Executor.Id,
                    IsTriggered = true,
                    ParentNodeId = container.Id,
                };

                container.Flows.Add(flow);
                await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.RootFlow);
            }
            else
            {
                //
            }

            await next(context);
        }


        private FlowContainer? GetContainer(Flow flow)
        {
            var nodeBase = flow.GetTriggeredNode();
            switch (nodeBase)
            {
                case FlowContainer container:
                    if (container.Flows.Count > 0)
                    {
                        foreach (var item in container.Flows)
                        {
                            return GetContainer(item);
                        }
                    }
                    else
                    {
                        return container;
                    }
                    return container;
            }

            return null;
        }
    }
}
