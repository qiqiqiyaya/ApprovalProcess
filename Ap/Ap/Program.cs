using Ap.Flow;

namespace Ap
{
    internal class Program
    {
        static void Main(string[] args)
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
            machine.ParallelAnd("FirstApprove", builder =>
            {
                builder.Configure("FirstApprove_1")
                    .JumpOut("Reject", "Return")
                    .JumpOut("ApproveCompleted", "SecondApprove")
                    .Permit("FirstApprove_1Pass", "SecondApprove_1")
                    .Finish("FirstApprove_1Pass");

                builder.Configure("FirstApprove_2")
                    .Permit("Reject", "Return")
                    .Permit("FirstApprove_2Pass", "SecondApprove_2")
                    .Finish("SecondApprove_2Pass");
            });

            // 第二级审批
            machine.Configure("SecondApprove")
                .Permit("SecondApprovedPass", "Completed")
                .Permit("Reject", "Return");

            machine.Id = "1";

            machine.Fire("Submitted");
            machine.Fire("FirstApprove_2Pass");
            Console.WriteLine("state: " + machine.CurrentState);
            Console.Read();
        }
    }
}
