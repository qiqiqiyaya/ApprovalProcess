using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;

namespace ApTest.FlowTest
{
    public class FlowApproverService : IApproverService
    {
        public ValueTask<List<string>> GetListAsync()
        {
            return new ValueTask<List<string>>(new List<string>() { "1", "2" });
        }

        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            throw new NotImplementedException();
        }
    }
}
