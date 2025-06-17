using Ap.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public abstract class AssignApprover : IEntryAction
    {
        public virtual async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            var list = await InvokeAsync(context);
            context.NextApproverList.AddRange(list);
            await next(context);
        }

        public abstract ValueTask<List<string>> InvokeAsync(EntryContext context);
    }

    public class SimpleAssignApprover(Func<EntryContext, ValueTask<List<string>>> assignAction) : AssignApprover
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return assignAction(context);
        }
    }

    public class DefaultAssignApprover()
        : SimpleAssignApprover(context => new ValueTask<List<string>>([context.Executor.Id]));
}
