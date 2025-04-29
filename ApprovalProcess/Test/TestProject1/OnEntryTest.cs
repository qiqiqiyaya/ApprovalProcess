using Ap.Core.Services;
using Ap.Core.StateMachine;

namespace TestProject1
{
    public enum PeState
    {
        /// <summary>
        /// 正在编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 第一次审批
        /// </summary>
        FirstApprove,
        /// <summary>
        /// 第二次审批
        /// </summary>
        SecondApprove,
        /// <summary>
        /// 退回
        /// </summary>
        Return,
        /// <summary>
        /// 审批完成
        /// </summary>
        Completed,
    }

    /// <summary>
    /// 触发器
    /// </summary>
    public enum PeStateTrigger
    {
        /// <summary>
        /// 提交
        /// </summary>
        Submitted,
        /// <summary>
        /// 第一次审批通过
        /// </summary>
        FirstApprovedPass,
        /// <summary>
        /// 第二次审批通过
        /// </summary>
        SecondApprovedPass,
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject,
        /// <summary>
        /// 重写
        /// </summary>
        Rewrite,
    }

    public class OnEntryTest : BaseTest
    {
        [Fact]
        public async Task OnEntryActionNameTest()
        {
            var machine = new StateMachine<string, string>("Edit");

            // 编辑 -> 提交
            machine.Configure("Edit")
                .Permit("Submitted", "FirstApprove");

            // 退回后，重写 -> 编辑状态
            machine.Configure("Return")
                .Permit("Rewrite", "Edit");

            // 第一级审批
            machine.Configure("FirstApprove")
                .OnEntry("TestEntryAction")
                .Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return");

            // 第二级审批
            machine.Configure("SecondApprove")
                .OnEntry("TestEntryAction")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            //var stateMachineService = GetRequiredService<IStateMachineService>();
            //await stateMachineService.SaveAsync(machine);
            await machine.Fire(new FireContext<string, string>(ServiceProvider, "Submitted"));

            Assert.Equal("FirstApprove", machine.CurrentState);
        }
    }
}
