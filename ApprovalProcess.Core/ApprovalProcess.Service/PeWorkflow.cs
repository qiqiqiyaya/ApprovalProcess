using ApprovalProcess.Core.Workflows;
using ApprovalProcess.Repository;
using ApprovalProcess.Share.Entities;
using Microsoft.EntityFrameworkCore;
using Stateless;
using Stateless.Reflection;

namespace ApprovalProcess.Service
{
    public class PeWorkflow : IWorkflow
    {
        private StateMachine<string, string> _workFlow;
        private readonly ApprovalProcessDbContext _dbContext;

        public PeWorkflow(ApprovalProcessDbContext dbContext)
        {
            _dbContext = dbContext;
            Definition();
        }

        /// <summary>
        /// 工作流定义
        /// </summary>
        private void Definition()
        {
            // state: Edit, FirstApprove , SecondApprove , Completed , Return
            // trigger: Submitted, FirstApprovedPass , SecondApprovedPass , Reject , Rewrite
            // 员工创建pe表单 -> 初始状态Edit状态 -> 触发提交 -> 进入第一级主管待审批状态 -> 触发审批通过 -> 进入第二级主管待审批状态 -> 触发审批通过 -> 完成状态
            // 主管待审批状态 -> 触发拒绝 -> 进入退回状态 -> 触发重写 -> 编辑状态
            State = "Edit";
            _workFlow = new StateMachine<string, string>(() => State, s => State = s);

            _workFlow.Configure("Edit")
                .Permit("Submitted", "FirstApprove");

            _workFlow.Configure("FirstApprove")
                .Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return");

            _workFlow.Configure("SecondApprove")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            _workFlow.Configure("Return")
                .Permit("Rewrite", "Edit");
        }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public string State { get; private set; }

        public async ValueTask<string> GetStateAsync(int workflowId)
        {
            var state = await _dbContext.Workflows
                .Where(x => x.Id == workflowId)
                .Select(s => s.State)
                .FirstAsync();
            return state;
        }

        /// <summary>
        /// 执行流程指定动作
        /// </summary>
        /// <param name="action">动作</param>
        /// <returns>下一步可执行的动作</returns>
        public List<string> Execute(string action)
        {
            _workFlow.Fire(action);
            return NextAction();
        }

        /// <summary>
        /// 流程下一步可执行的动作
        /// </summary>
        /// <returns>下一步可执行的动作</returns>
        public List<string> NextAction()
        {
            return _workFlow.PermittedTriggers?.ToList() ?? new List<string>();
        }

        public StateMachineInfo GetInfo()
        {
            return _workFlow.GetInfo();
        }

        public async ValueTask<WorkflowEntity> CreateAsync()
        {
            var entity = new WorkflowEntity()
            {
                State = State,
                Type = WorkflowType.Pe
            };
            await _dbContext.Workflows.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
