using Ap.Flow;
using Ap.Flow.Behaviours;

namespace Ap.TestCase
{
    public static class OrFork
    {
        public static void OrForkTest()
        {
            var machine = new StateMachine();

            // 编辑 -> 提交
            machine.Start("Edit")
                .Then("FirstApprove", sr =>
                {
                    sr.SkipTo("Completed")
                        .SkipTo("ThirdApprove");
                })
                .Then("SecondApprove")
                .BranchAnd(branch =>
                {
                    branch.Then("SecondApprove_A1")
                        .Then("SecondApprove_A2");

                    branch.Then("SecondApprove_B1")
                        .Then("SecondApprove_B2");
                })
                .Join("ThirdApprove")
                .Complete("Completed");

            machine.Trigger("Submit");
            var aa = machine.GetTriggers();
            machine.Trigger(BehaviourConst.Approve);
            aa = machine.GetTriggers();
            machine.Trigger(BehaviourConst.AndBegin + "_SecondApprove_A1");
            aa = machine.GetTriggers();
            //machine.Trigger(BehaviourConst.SkipTo + "_ThirdApprove");


            Console.WriteLine("state: " + machine.CurrentState);
            Console.Read();
        }
    }
}
