using Sm.Core.Services;
using Sm.Core.StateMachine;
using System;

namespace StateMachineTest
{
    public class UnitTest1 : BaseTest
    {
        private StateMachineConfiguration _configuration = new StateMachineConfiguration();

        /// <summary>
        /// 配置一个两级审批流程
        /// </summary>
        /// <returns></returns>
        public async Task ConfigureTwoLevelAp()
        {
            var machine = new StateMachine<string, string>("Edit");

            // 编辑 -> 提交
            machine.Configure("Edit")
                .PermitReentry("Edited")
                .Permit("Submitted", "FirstApprove");

            // 退回后，重写 -> 编辑状态
            machine.Configure("Return")
                .Permit("Rewrite", "Edit");

            // 第一级审批
            machine.ParallelOr("FirstApprove", builder =>
            {
                builder.Configure("FirstApprove_1")
                    .Permit("Reject", "Return")
                    .Permit("FirstApprove_1Pass", "ParallelPass")
                    .Permit("ApproveCompleted", "Completed");

                builder.Configure("FirstApprove_2")
                    .Permit("Reject", "Return")
                    .Permit("FirstApprove_2Pass", "SecondApprove_2")
                    .Permit("SecondApprove_2Pass", "ParallelPass");
            });

            // 第二级审批
            machine.Configure("SecondApprove")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            machine.Id = "1";
            _configuration.Add(machine);

            //await machine.Fire(new FireContext<string, string>(ServiceProvider, "Submitted"));
            //await machine.Fire(new FireContext<string, string>(ServiceProvider, "FirstApprove_1Pass"));
        }

        [Fact]
        public async Task Create()
        {
            await ConfigureTwoLevelAp();

            var sm = _configuration.Get("1");
            var creator = new Creator() { Id = "1", StateMachineId = sm.Id };

            await sm.Fire("Edited");
        }
    }
}
