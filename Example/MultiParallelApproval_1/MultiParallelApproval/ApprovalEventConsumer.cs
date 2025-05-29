using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParallelApproval
{
    public class ApprovalEventConsumer : IIntegrationEventHandler<ApprovalTaskUpdatedEvent>
    {
        private readonly ParallelApprovalEngine _engine;

        public async Task Handle(ApprovalTaskUpdatedEvent @event)
        {
            var context = LoadProcessContext(@event.ProcessInstanceId);

            if (context.CurrentNode.NodeType == ApprovalNodeType.Parallel)
            {
                var completionStatus = _engine.CheckParallelCompletion(context);

                if (completionStatus.IsCompleted)
                {
                    await _engine.ProcessPostParallelActions(context, completionStatus);
                }
            }
        }
    }
}
