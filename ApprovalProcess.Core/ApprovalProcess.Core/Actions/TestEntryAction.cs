using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
    public class TestEntryAction : IEntryAction
    {
        public string Id { get; set; }
        public string Name { get; }

        public ValueTask InvokeAsync(EntryActionContext context, Func<EntryActionContext, ValueTask> next)
        {
            return next(context);
        }
    }
}
