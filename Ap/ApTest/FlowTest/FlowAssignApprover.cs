using Ap.Core.Actions;
using Ap.Core.Definitions;

namespace ApTest.FlowTest
{
    public class FlowAssignApprover : AssignApprover
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return new ValueTask<List<string>>(new List<string>());
        }
    }

    public class FlowAssignApproverService111 : AssignApprover
    {
        public override ValueTask<List<string>> InvokeAsync(EntryContext context)
        {
            return new ValueTask<List<string>>(new List<string>());
        }
    }
}
