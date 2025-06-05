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

            //builder.Then("FirstApprove")
            //    .Then("SecondApprove")
            //    .If(() => true, "aaa", "bbb")
            //    //.Then("ThirdApprove")
            //    .Complete();
            //IStateSet stateSet = builder.Build();
            //stateSet.ExecuteTrigger(TransitionConst.Submit);
            //stateSet.ExecuteTrigger(TransitionConst.Approve);
            //stateSet.ExecuteTrigger(TransitionConst.Approve);
            //Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            //var str = stateSet.GetTrigger();

            //stateSet.ExecuteTrigger(TransitionConst.Approve);
            ////stateSet.ExecuteTrigger(TransitionConst.Approve);
            //Console.WriteLine("IsEnd:" + stateSet.IsEnd);
            //var aa = stateSet.GetTrigger();


            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .If(() => true,
                    builderProvider => builderProvider.Create("aaa").Then("aaaApprove"),
                    builderProvider => builderProvider.Create("bbb").Then("bbbApprove"));

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            var str = stateSet.GetTrigger();

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            Console.WriteLine("IsEnd:" + stateSet.IsEnd);
            var aa = stateSet.GetTrigger();
        }
    }
}
