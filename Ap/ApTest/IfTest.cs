using Ap.Core.Builders;
using Ap.Core.Services.Interfaces;

namespace ApTest
{
    public class IfFlowPreBuilder : IPreBuilder
    {
        public const string FlowName = "IfFlowFlowTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");
            builder.Name = FlowName;

            builder.If(context =>
                {
                    return true;
                },
                    ifBuilderProvider => ifBuilderProvider.Create("aaa"),
                ifBuilderProvider => ifBuilderProvider.Create("bbb"))
                .Then("SecondApprove")
                .Then("ThirdApprove");

            builder.AssignApprover("aaa", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "11" });
            });
            builder.AssignApprover("bbb", context =>
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

    public class IfTest : Base
    {
        [Fact]
        public async Task Test1()
        {
            var user = new TestUser();
            var executionService = GetService<IExecutionService>();
            var flow = await executionService.InvokeAsync(user, IfFlowPreBuilder.FlowName);

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
