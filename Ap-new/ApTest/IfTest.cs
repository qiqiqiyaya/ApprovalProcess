using Ap.Core.Behaviours;
using Ap.Core.Builders;
using Ap.Core.Definitions;

namespace ApTest
{
    public class IfTest
    {

        [Fact]
        public void Test1()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();
            var builder = provider.Create("edit");
            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .If(() => true, "aaa", "bbb")
                .Complete();
            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            var str = stateSet.GetTrigger();

            stateSet.ExecuteTrigger(TransitionConst.Approve);
            //stateSet.ExecuteTrigger(TransitionConst.Approve);
            var aa = stateSet.GetTrigger();
        }

        [Fact]
        public void Test2()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();

            var builder = provider.Create("edit");
            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .If(() => true,
                    builderProvider => builderProvider.Create("aaa").Then("aaaApprove"),
                    builderProvider => builderProvider.Create("bbb").Then("bbbApprove"))
                .Then("ThirdApprove");

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            var str = stateSet.GetTrigger();

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            var aa = stateSet.GetTrigger();
        }
    }
}
