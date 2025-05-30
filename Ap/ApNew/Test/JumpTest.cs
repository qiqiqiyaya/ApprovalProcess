using ApNew.Nodes;
using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Builders;
using ApNew.Nodes.Core;

namespace ApNew.Test
{
    public class JumpTest
    {
        public static void Test()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();

            var builder = provider.Create("edit");

            builder.Then("FirstApprove")
                .Jump("SecondApprove", "FifthApprove")
                .Then("ThirdApprove")
                .Then("FourthApprove")
                .Then("FifthApprove")
                .Complete();

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            var aa = stateSet.GetTrigger();

            stateSet.ExecuteTrigger(TransitionConst.Jump);
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            aa = stateSet.GetTrigger();
        }
    }
}
