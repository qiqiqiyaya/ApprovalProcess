using System;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Actions
{
    /// <summary>
    /// 设置下一个状态审批人操作
    /// </summary>
    public class SetNextApproverAction : IExitAction
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ValueTask InvokeAsync(ExitActionContext context, Func<ExitActionContext, ValueTask> next)
        {
            return next(context);
        }
    }
}
