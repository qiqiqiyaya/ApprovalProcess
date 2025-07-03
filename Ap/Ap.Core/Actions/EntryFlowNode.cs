using Ap.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ap.Core.Actions
{
    public class EntryFlowNode : IEntryAction
    {
        public ValueTask InvokeAsync(EntryContext context, Func<EntryContext, ValueTask> next)
        {
            throw new NotImplementedException();
        }
    }
}
