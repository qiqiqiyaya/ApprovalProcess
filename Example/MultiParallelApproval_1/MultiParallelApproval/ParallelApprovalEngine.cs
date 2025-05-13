using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParallelApproval
{
    public class ParallelApprovalEngine
    {
        private readonly IApprovalTaskRepository _taskRepository;

        public async Task ProcessParallelNode(ApprovalContext context)
        {
            // 生成并行任务
            var approvers = ResolveApprovers(context.CurrentNode);
            var tasks = CreateParallelTasks(approvers, context);

            await _taskRepository.BulkCreateTasks(tasks);

            // 设置监听器
            var completionSource = new TaskCompletionSource<bool>();
            RegisterCompletionTriggers(context, completionSource);

            await completionSource.Task;
            ProceedToNextNodes(context);
        }

        private List<Approver> ResolveApprovers(ApprovalNode node)
        {
            // 实现动态审批人解析逻辑
            // 支持：角色、部门、特定人员、上级领导等
        }

        private void RegisterCompletionTriggers(ApprovalContext context,
            TaskCompletionSource<bool> source)
        {
            _taskRepository.OnTaskUpdated += (sender, args) =>
            {
                if (IsCompletionConditionMet(context))
                {
                    source.SetResult(true);
                }
            };
        }

        private bool IsCompletionConditionMet(ApprovalContext context)
        {
            var currentTasks = GetCurrentTasks(context);

            return context.CurrentNode.ParallelStrategy.Condition switch
            {
                CompletionCondition.AllApproved =>
                    currentTasks.All(t => t.Status == ApprovalStatus.Approved),
                CompletionCondition.AnyApproved =>
                    currentTasks.Any(t => t.Status == ApprovalStatus.Approved),
                CompletionCondition.Majority =>
                    currentTasks.Count(t => t.Status == ApprovalStatus.Approved)
                    > currentTasks.Count / 2,
                _ => false
            };
        }
    }
}
