using Ap.Core.Definitions;
using Ap.Core.Services;

namespace ApTest.FlowTest
{
    public class FlowAssignApproverService : AssignApproverService
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return new ValueTask<List<string>>(new List<string>());
        }
    }

    public class FlowAssignApproverService111 : AssignApproverService
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return new ValueTask<List<string>>(new List<string>());
        }
    }
}
