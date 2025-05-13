using System.Data;
using System.Reflection.PortableExecutable;
using Stateless;

namespace MultiParallelApproval_2Stateless
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StateMachine<string, string> sm = new StateMachine<string, string>("Edit");

            // 编辑 -> 提交
            sm.Configure("Edit")
                .Permit("Submitted", "FirstApprove");

            // 退回后，重写 -> 编辑状态
            sm.Configure("Return")
                .Permit("Rewrite", "Edit");

            // 第一级审批 ,  多人并行审批
            sm.Configure("FirstApprove")
                .Permit("FirstApprovedPass", "SecondApprove")
                .Permit("Reject", "Return")
                .SubstateOf("FirstApprove_1")
                .SubstateOf("FirstApprove_2");
            // FirstApprove_1
            sm.Configure("FirstApprove_1")
                .Permit("FirstApprovePass_1", "Pass")
                .Permit("Reject", "Return");
            // FirstApprove_2
            sm.Configure("FirstApprove_2")
                .Permit("FirstApprovePass_2", "Pass")
                .Permit("Reject", "Return");


            // 第二级审批
            sm.Configure("SecondApprove")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");
        }
    }
}
