using ApNew.Nodes;
using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Builders;
using ApNew.Nodes.Core;

namespace ApNew.Test
{
    public class IfTest
    {
        public static void Test()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();

            var builder = provider.Create("edit");

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .BranchAnd(branch =>
                {
                    branch.New("SecondApprove_A1", "1")
                        .Then("SecondApprove_A2");
                    branch.New("SecondApprove_B1", "2")
                        .Then("SecondApprove_B2");
                })
                .Join("ThirdApprove")
                .Complete();

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(TransitionConst.Reject);
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            Console.WriteLine("Triggers:" + string.Join(',', stateSet.GetTrigger()));

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "1", Trigger = TransitionConst.Approve });
            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "1", Trigger = TransitionConst.Reject });
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            Console.WriteLine("Triggers:" + string.Join(',', stateSet.GetTrigger()));

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "1", Trigger = TransitionConst.Approve });
            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "1", Trigger = TransitionConst.Approve });

            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "2", Trigger = TransitionConst.Approve });
            stateSet.ExecuteTrigger(new TriggerParameter() { StateSetId = "2", Trigger = TransitionConst.Approve });

            stateSet.ExecuteTrigger(TransitionConst.Approve);
            Console.WriteLine("IsEnd:" + stateSet.IsEnd);
            Console.WriteLine("Triggers:" + string.Join(',', stateSet.GetTrigger()));
        }
    }
}
