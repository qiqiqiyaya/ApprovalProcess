using ApprovalProcess.Share.Entities;
using Stateless.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Workflows
{
    public interface IWorkflow
    {
        ValueTask<string> GetStateAsync(int workflowId);

        /// <summary>
        /// 执行流程指定动作
        /// </summary>
        /// <param name="action">动作</param>
        /// <returns>下一步可执行的动作</returns>
        List<string> Execute(string action);

        /// <summary>
        /// 流程下一步可执行的动作
        /// </summary>
        /// <returns>下一步可执行的动作</returns>
        List<string> NextAction();

        StateMachineInfo GetInfo();

        ValueTask<WorkflowEntity> CreateAsync();
    }
}
