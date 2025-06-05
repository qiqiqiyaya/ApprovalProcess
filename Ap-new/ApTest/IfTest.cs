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
            var dictionary = stateSet.GetTrigger();

            var trigger = dictionary.GetTrigger("aaa", TransitionConst.Approve);
            Assert.NotNull(trigger);

            stateSet.ExecuteTrigger(trigger);
            Assert.True(stateSet.IsEnd);
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
                    builderProvider => builderProvider.Create("bbb").Then("bbbApprove"));

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            var dictionary = stateSet.GetTrigger();
            var trigger1 = dictionary.GetTrigger("aaa", TransitionConst.Approve);
            Assert.NotNull(trigger1);
            stateSet.ExecuteTrigger(trigger1);

            dictionary = stateSet.GetTrigger();
            var trigger2 = dictionary.GetTrigger("aaaApprove", TransitionConst.Approve);
            Assert.NotNull(trigger2);
            stateSet.ExecuteTrigger(trigger2);
            Assert.True(stateSet.IsEnd);
        }
    }
}
