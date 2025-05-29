using Ap.Share.Actions;
using Sm.Core.Actions.Entry;
using Sm.Share.Actions;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Actions.Entry
{
    [ActionName(ExecutableActionNames.OnEntrySetCleanNextApprover)]
    public class CleanNextApprover : IEntryAction<string, string>
    {
        public ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
        public string Name { get; }
    }
}
