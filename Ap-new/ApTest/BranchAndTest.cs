using Ap.Core.Behaviours;
using Ap.Core.Builders;
using Ap.Core.Definitions;
using Ap.Core.Extensions;

namespace ApTest
{
    public class BranchAndTest : Base
    {
        [Fact]
        public void Test()
        {
            var provider = GetService<IStateSetBuilderProvider>();
            var builder = provider.Create("edit");

            string aId = "";
            string bId = "";

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .BranchAnd(branch =>
                {
                    var builderA = branch.New("SecondApprove_A1")
                        .Then("SecondApprove_A2");
                    aId = builderA.Id;

                    var builderB = branch.New("SecondApprove_B1", "2")
                        .Then("SecondApprove_B2");
                    bId = builderB.Id;
                })
                .Then("ThirdApprove")
                .Complete();

            builder.ConfigureEntry("FirstApprove", async context =>
            {
                await Task.Delay(100);
            });

            builder.ConfigureEntry("SecondApprove_B1", async context =>
            {
                await Task.Delay(100);
            });

            builder.ConfigureEntry("SecondApprove_B2", async context =>
            {
                await Task.Delay(100);
            });

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(TransitionConst.Reject);

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(aId, TransitionConst.Approve);
            var triggerDictionary = stateSet.GetTrigger();

            Assert.True(triggerDictionary.Count > 0);

            stateSet.ExecuteTrigger(aId, TransitionConst.Reject);

            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            stateSet.ExecuteTrigger(aId, TransitionConst.Approve);
            stateSet.ExecuteTrigger(aId, TransitionConst.Approve);

            stateSet.ExecuteTrigger(bId, TransitionConst.Approve);
            stateSet.ExecuteTrigger(bId, TransitionConst.Approve);

            stateSet.ExecuteTrigger(TransitionConst.Approve);
            Assert.True(stateSet.IsEnd);
        }
    }
}