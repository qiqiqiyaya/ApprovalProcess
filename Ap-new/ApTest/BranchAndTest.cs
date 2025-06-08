using Ap.Core.Behaviours;
using Ap.Core.Definitions;

namespace ApTest
{
    public class BranchAndTest : Base
    {
        [Fact]
        public void Test()
        {
            var builder = StateSetBuilderProvider.Create("edit");
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

            builder.ConfigureEntry("FirstApprove", async (context) =>
            {
                await Task.Delay(2000);
            });

            builder.ConfigureEntry("SecondApprove_B1", async (context) =>
            {
                await Task.Delay(2000);
            });

            builder.ConfigureEntry("ThirdApprove", async (context) =>
            {
                await Task.Delay(2000);
            });

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(ApCoreTriggers.Submit);
            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);

            stateSet.ExecuteTrigger(ApCoreTriggers.Reject);

            stateSet.ExecuteTrigger(ApCoreTriggers.Submit);
            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);

            stateSet.ExecuteTrigger(aId, ApCoreTriggers.Approve);
            var triggerDictionary = stateSet.GetTrigger();

            Assert.True(triggerDictionary.Count > 0);

            stateSet.ExecuteTrigger(aId, ApCoreTriggers.Reject);

            stateSet.ExecuteTrigger(ApCoreTriggers.Submit);
            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);

            stateSet.ExecuteTrigger(aId, ApCoreTriggers.Approve);
            stateSet.ExecuteTrigger(aId, ApCoreTriggers.Approve);

            stateSet.ExecuteTrigger(bId, ApCoreTriggers.Approve);
            stateSet.ExecuteTrigger(bId, ApCoreTriggers.Approve);

            stateSet.ExecuteTrigger(ApCoreTriggers.Approve);
            Assert.True(stateSet.IsEnd);
        }
    }
}