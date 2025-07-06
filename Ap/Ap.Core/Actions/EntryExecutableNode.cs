using Ap.Core.Definitions;
using Ap.Core.Definitions.Actions;
using Ap.Core.Exceptions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Actions;

/// <summary>
/// Updates the flow state and approvers.
/// </summary>
public class EntryExecutableNode : IEntryAction
{
    public async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
    {
        var currentFlow = context.GetCurrentFlow();

        var node = new Node(currentFlow)
        {
            Id = Guid.NewGuid().ToString("N"),
            StateName = context.State.Name,
            StateId = context.StateTrigger.StateDetail.Id,
            ExecutorId = context.Executor.Id,
            StateSetId = context.CurrentStateSet.Id,
            ParentNodeId = currentFlow.Id
        };

        var actions = (List<ApAction>)context.Properties[EntryContext.EntryActionsProperty];
        node.Entry(actions);
        node.IsTriggered = true;

        await next(context);

        if (context.NextApproverList.Count == 0)
        {
            throw new ApException("No approvers assigned for the flow.");
        }

        await context.AddNode(node);
    }
}
