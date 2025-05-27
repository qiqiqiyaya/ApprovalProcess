using ApNew.Nodes;
using ApNew.Nodes.Behaviours;

namespace ApNew.Test
{
    public class Test1
    {
        public static void Test()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();

            var builder = provider.Create("edit");

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Branch(branch =>
                {
                    branch.New("SecondApprove_A1", "1")
                        .Then("SecondApprove_A2");
                    branch.New("SecondApprove_B1", "2")
                        .Then("SecondApprove_B2");
                })
                .Join("ThirdApprove")
                .Complete("Completed");

            IStateTrigger stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "1", Trigger = TransitionConst.Approve });

        }
    }
}
