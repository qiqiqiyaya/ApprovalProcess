using Ap.Core.Definitions;
using Ap.Core.Definitions.Actions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Actions
{
    public class ExitExecutableNode : IExitAction
    {
        public virtual async ValueTask InvokeAsync(ExitContext context, Func<ExitContext, ValueTask> next)
        {
            var node = (Node)context.GetCurrentFlow().GetTriggeredNode()!;
            node.UpdateTime = DateTime.UtcNow;
            var trigger = new OutputTrigger
            {
                ExecuteTime = context.TriggeredTime,
                Id = Guid.NewGuid().ToString("N"),
                Trigger = context.StateTrigger.Trigger
            };

            var actions = (List<ApAction>)context.Properties[ExitContext.ExitActionsProperty];
            node.Exit(trigger, actions);
            node.ExecutorId = context.Executor.Id;
            node.IsTriggered = false;

            await context.GetRequiredService<IFlowManager>().UpdateFlowAsync(context.RootFlow);
            await next(context);
        }
    }


}
