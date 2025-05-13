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

            NextApproverConfiguration configuration = new NextApproverConfiguration
            {
                Rule = ApprovalRule.ApprovedByOrg
            };

            // 编辑 -> 提交
            machine.Configure("Edit")
                .OnEntry(ExecutableActionNames.OnEntryTriggerRecording)
                .Permit("Submitted", "FirstApprove");

            // 退回后，重写 -> 编辑状态
            machine.Configure("Return")
                .OnEntry(ExecutableActionNames.OnEntryTriggerRecording)
                .OnEntry(ExecutableActionNames.OnEntrySetCleanNextApprover)
                .Permit("Rewrite", "Edit");

            // 第一级审批
            machine.Configure("FirstApprove")
                .OnEntry(ExecutableActionNames.OnEntryTriggerRecording)
                .OnEntry(ExecutableActionNames.OnEntrySetNextApprover, configuration)
                //.Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return")
                .Children("ParallelPass", builder =>
                {
                    builder.New("FirstApprove_1")
                        .Permit("Reject", "Return")
                        .Permit("FirstApprove_1Pass", "ParallelPass");

                    builder.New("FirstApprove_2")
                        .Permit("Reject", "Return")
                        .Permit("FirstApprove_2Pass", "SecondApprove_2")
                        .Permit("SecondApprove_2Pass", "ParallelPass");
                });

            // 第二级审批
            machine.Configure("SecondApprove")
                .OnEntry(ExecutableActionNames.OnEntryTriggerRecording)
                .OnEntry(ExecutableActionNames.OnEntrySetNextApprover, configuration)
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            var service = GetRequiredService<IStateMachineService>();
            var entity = await service.SaveAsync(machine);

            await machine.Fire(new FireContext<string, string>(ServiceProvider, "Submitted"));
            await machine.Fire(new FireContext<string, string>(ServiceProvider, "FirstApprove_1Pass"));

            Assert.NotNull(entity);
            Assert.NotNull(entity.Id);
        }
    }
}
