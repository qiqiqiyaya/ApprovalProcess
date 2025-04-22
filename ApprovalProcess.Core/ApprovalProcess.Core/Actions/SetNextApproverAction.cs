using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
    /// <summary>
    /// 设置下一个状态审批人操作
    /// </summary>
    public class SetNextApproverAction : SmAction
    {
        public override ValueTask InvokeAsync(ActionContext context, Func<ActionContext, ValueTask> next)
        {
            throw new NotImplementedException();
        }
    }
}
