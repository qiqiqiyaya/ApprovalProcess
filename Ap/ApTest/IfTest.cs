using Ap.Core.Behaviours;
using Ap.Core.Definitions;

namespace ApTest
{
    public class IfTest : Base
    {
        [Fact]
        public void Test1()
        {
            //var builder = StateSetBuilderProvider.Create("edit");
            //builder.Then("FirstApprove")
            //    .Then("SecondApprove")
            //    .If(() => true, "aaa", "bbb")
            //    .Complete();
            //IStateSet stateSet = builder.Build();
            //stateSet.ExecuteTrigger(ApCoreTriggers.Submit);
            //stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            //stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            //var dictionary = stateSet.GetTrigger();

            //var trigger = dictionary.GetTrigger("aaa", ApCoreTriggers.Approve);
            //Assert.NotNull(trigger);

            //stateSet.ExecuteTrigger(trigger);
            //Assert.True(stateSet.IsEnd);
        }

        [Fact]
        public void Test2()
        {
            //var builder = StateSetBuilderProvider.Create("edit");
            //builder.Then("FirstApprove")
            //    .Then("SecondApprove")
            //    .If(() => true,
            //        builderProvider => builderProvider.Create("aaa").Then("aaaApprove"),
            //        builderProvider => builderProvider.Create("bbb").Then("bbbApprove"));

            //IStateSet stateSet = builder.Build();
            //stateSet.ExecuteTrigger(ApCoreTriggers.Submit);
            //stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            //stateSet.ExecuteTrigger(ApCoreTriggers.Approve);

            //var dictionary = stateSet.GetTrigger();
            //var trigger1 = dictionary.GetTrigger("aaa", ApCoreTriggers.Approve);
            //Assert.NotNull(trigger1);
            //stateSet.ExecuteTrigger(trigger1);

            //dictionary = stateSet.GetTrigger();
            //var trigger2 = dictionary.GetTrigger("aaaApprove", ApCoreTriggers.Approve);
            //Assert.NotNull(trigger2);
            //stateSet.ExecuteTrigger(trigger2);
            //Assert.True(stateSet.IsEnd);
        }
    }
}
