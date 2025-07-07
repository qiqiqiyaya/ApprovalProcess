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
            var containerId = (string)context.Properties[StateSetContainerBase.StateSetContainerIdProperty];

            var container = GetContainer(context.RootFlow, containerId);
            var flow = container!.Flows.Find(x => x.StateSetId == context.CurrentStateSet.Id);

            flow.UpdateTime = DateTime.UtcNow;
            flow.ExecutorId = context.Executor.Id;
            flow.IsTriggered = false;
            flow.FlowStatus = FlowStatus.Completed;

            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.RootFlow);
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
