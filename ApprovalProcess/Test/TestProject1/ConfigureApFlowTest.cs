using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ap.Share.Actions.Entry.NextApprover;
using Sm.Core.StateMachine;
using Test.Common;
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
                .OnEntry("TestEntryAction")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");


        }
    }
}
