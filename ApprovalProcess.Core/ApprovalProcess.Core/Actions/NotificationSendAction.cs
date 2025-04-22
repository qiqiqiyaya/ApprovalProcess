using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
    public class NotificationSendAction : SmAction
    {
        public override ValueTask InvokeAsync(ActionContext context, Func<ActionContext, ValueTask> next)
        {
            throw new NotImplementedException();
        }
    }
}
