using Ap.Core.Builders;
using Ap.Core.Services.Interfaces;

namespace ApTest
{
    public class FlowPreBuilder : IPreBuilder
    {
        public const string FlowName = "FlowTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Then("ThirdApprove");

            builder.Name = FlowName;

            builder.EntryAction("edit", context =>
            {

            });

            builder.EntryAction("edit", context =>
            {

            });

            builder.AssignApprover("FirstApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "11" });
            });
            builder.AssignApprover("SecondApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "22" });
            });
            builder.AssignApprover("ThirdApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "33" });
            });

            return builder;
        }
    }

    public class FlowTest1 : Base
    {
        private async Task ExecFlow(IUser user, string flowId)
        {
            var flowManager = GetService<IFlowManager>();
            var executionService = GetService<IExecutionService>();
            var flow = await flowManager.GetFlowAsync(flowId);
            var actions = await executionService.GetTriggerAsync(flow);
            var trigger = actions[0];
            await executionService.InvokeAsync(user, flow, trigger);
        }

        [Fact]
        public async Task CreateTest()
        {
            var user = new TestUser();
            var executionService = GetService<IExecutionService>();
            var flow = await executionService.InvokeAsync(user, FlowPreBuilder.FlowName);

            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);
            await ExecFlow(user, flow.Id);

            var flowManager = GetService<IFlowManager>();
            var userFlow = await flowManager.GetUserFlow(flow.Id);

            Assert.False(userFlow.Flow.IsTriggered);
            Assert.True(userFlow.Flow.Nodes.Count >= 4);
        }
    }
}
