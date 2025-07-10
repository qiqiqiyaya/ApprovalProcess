using Ap.Core.Builders;
using Ap.Core.Services.Interfaces;

namespace ApTest
{

    public class BranchAndBuildTest : IPreBuilder
    {
        public const string FlowName = "BranchAndBuildTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit")
                .Then("FirstApprove")
                .Then("SecondApprove")
                .BranchAnd(branch =>
                {
                    branch.New("SecondApprove_A1")
                        .Then("SecondApprove_A2");

                    branch.New("SecondApprove_B1")
                        .Then("SecondApprove_B2");
                })
                .Then("ThirdApprove");

            builder.Name = FlowName;
            return builder;
        }
    }

    public class BranchAndTest : Base
    {
        [Fact]
        public async Task Test()
        {
            var user = new TestUser();
            var executionService = GetService<IExecutionService>();
            var flow = await executionService.InvokeAsync(user, BranchAndBuildTest.FlowName);

            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);

            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);

            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);

            var flowManager = GetService<IFlowManager>();
            var userFlow = await flowManager.GetUserFlow(flow.Id);
        }

        private async Task ExecFlow(IUser user, string flowId)
        {
            var flowManager = GetService<IFlowManager>();
            var executionService = GetService<IExecutionService>();
            var flow = await flowManager.GetFlowAsync(flowId);
            var actions = await executionService.GetTriggerAsync(flow);
            var trigger = actions[0];
            await executionService.InvokeAsync(user, flow, trigger);
        }
    }
}