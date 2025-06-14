using Ap.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public abstract class AssignApproverService : IEntryAction
    {
        public virtual async ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            var list = await InvokeAsync(context);
            context.NextApproverList.AddRange(list);
            await next(context);
        }

        public abstract ValueTask<List<string>> InvokeAsync(EntryContext context);
    }

    public class SimpleAssignApproverService(Func<EntryContext, ValueTask<List<string>>> assignAction) : AssignApproverService
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return assignAction(context);
        }
    }
}
