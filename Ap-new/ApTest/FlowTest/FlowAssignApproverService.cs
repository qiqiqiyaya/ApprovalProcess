using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;

namespace ApTest.FlowTest
{
    public class FlowAssignApproverService : IAssignApproverService
    {
        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            return next(context);
        }
    }

    public class FlowAssignApproverService111 : IAssignApproverService
    {
        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            return next(context);
        }
    }
}
