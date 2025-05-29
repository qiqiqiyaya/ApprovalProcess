using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace MultiParallelApproval_2Stateless
{
    // 安装NuGet包：Stateless
    public class ApprovalStateMachine
    {
        private readonly StateMachine<State, Trigger> _machine;
        private readonly ApprovalProcessContext _context;

        public ApprovalStateMachine(ApprovalProcessContext context)
        {
            _context = context;
            _machine = new StateMachine<State, Trigger>(() => _context.CurrentState,
                                                       s => _context.CurrentState = s);

            ConfigureMachine();
        }

        private void ConfigureMachine()
        {
            // 初始状态配置
            _machine.Configure(State.Draft)
                .Permit(Trigger.Submit, State.ParallelApproving);

            // 并行审批状态配置
            _machine.Configure(State.ParallelApproving)
                .OnEntry(StartParallelApproval)  // 进入状态时触发并行任务
                .PermitReentry(Trigger.Approve)  // 允许同状态转换
                .Permit(Trigger.Complete, State.Completed)
                .Permit(Trigger.Reject, State.Rejected);
        }

        private void StartParallelApproval()
        {
            // 生成并行审批任务
            var tasks = _context.CurrentNode.Approvers
                .Select(a => new ApprovalTask(a))
                .ToList();

            // 使用线程安全集合
            _context.PendingTasks = new ConcurrentBag<ApprovalTask>(tasks);

            // 启动后台监控
            Task.Run(() => MonitorParallelTasks());
        }

        private async Task MonitorParallelTasks()
        {
            while (!_context.IsParallelCompleted)
            {
                await Task.Delay(500);

                // 检查完成条件
                var completed = _context.PendingTasks.All(t => t.IsCompleted);
                var anyRejected = _context.PendingTasks.Any(t => t.IsRejected);

                if (anyRejected)
                {
                    _machine.Fire(Trigger.Reject);
                    break;
                }

                if (completed && CheckCompletionPolicy())
                {
                    _machine.Fire(Trigger.Complete);
                    break;
                }
            }
        }

        private bool CheckCompletionPolicy()
        {
            // 实现策略模式
            return _context.CurrentNode.Policy switch
            {
                ParallelPolicy.All => _context.PendingTasks.All(t => t.IsApproved),
                ParallelPolicy.Any => _context.PendingTasks.Any(t => t.IsApproved),
                ParallelPolicy.Majority => _context.PendingTasks.Count(t => t.IsApproved)
                                         > _context.PendingTasks.Count / 2,
                _ => false
            };
        }
    }
}
