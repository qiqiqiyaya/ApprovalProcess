using Ap.Share.Actions.Entry.NextApprover;
using Sm.Core.Services;
using Sm.Core.StateMachine;
using ExecutableActionNames = Ap.Share.Actions.ExecutableActionNames;

namespace TestProject1
{
    public class ConfigureApFlowTest : BaseTest
    {
        /// <summary>
        /// 配置一个两级审批流程
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ConfigureTwoLevelAp()
        {
            var machine = new StateMachine<string, string>("Edit");

            // 编辑 -> 提交
            machine.Configure("Edit")
                .Permit("Submitted", "FirstApprove");

            // 退回后，重写 -> 编辑状态
            machine.Configure("Return")
                .OnEntry(ExecutableActionNames.OnEntrySetCleanNextApprover)
                .Permit("Rewrite", "Edit");

            NextApproverConfiguration configuration = new NextApproverConfiguration
            {
                Rule = ApprovalRule.ApprovedByOrg
            };

            // 第一级审批
            machine.Configure("FirstApprove")
                .OnEntry(ExecutableActionNames.OnEntrySetNextApprover, configuration)
                .Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return");

            // 第二级审批
            machine.Configure("SecondApprove")
                .OnEntry(ExecutableActionNames.OnEntrySetNextApprover, configuration)
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            var service = GetRequiredService<IStateMachineService>();
            var entity = await service.SaveAsync(machine);

            Assert.NotNull(entity);
            Assert.NotNull(entity.Id);
        }
    }
}
