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
            if (context.RootFlow == null!)
            {
                await FirstEntry(context);
            }
            else
            {
                var containerId = (string)context.Properties[StateSetContainerBase.StateSetContainerIdProperty];

                var container = GetContainer(context.RootFlow, containerId);
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
                    throw new Exception($"container is null. {nameof(EntryFlowNode)}.{nameof(InvokeAsync)}");
                }
            }

            await next(context);
        }

        private async ValueTask FirstEntry(EntryContext context)
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
                ParentNodeId = null
            };

            context.RootFlow = flow;
            await context.GetRequiredService<IFlowManager>().CreateUserFlowAsync(context.Executor, flow);
        }

        private FlowContainer? GetContainer(Flow flow, string containerId)
        {
            var nodeBase = flow.GetTriggeredNode();
            switch (nodeBase)
            {
                case FlowContainer container:
                    if (container.StateId == containerId) return container;
                    if (container.Flows.Count > 0)
                    {
                        foreach (var item in container.Flows)
                        {
                            return GetContainer(item, containerId);
                        }
                    }

                    return container;
            }

            return null;
        }
    }
}
