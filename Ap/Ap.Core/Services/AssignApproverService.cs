using Ap.Core.Definitions;
using Ap.Core.Services.Models;
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

            //foreach (var objectId in list)
            //{
            //    var np = new NextApprover()
            //    {
            //        Id = Guid.NewGuid().ToString("N"),
            //        ObjectId = objectId,
            //        FlowId = context.Flow.Id,
            //        CreateTime = DateTime.UtcNow
            //    };

            //    context.Flow.Approvers
            //}


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
