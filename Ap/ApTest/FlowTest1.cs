using Ap.Core.Builders;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;

namespace ApTest
{
    public class FlowPreBuilder : IPreBuilder
    {
        public const string FlowName = "FlowTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");

            builder.Id = "1";
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
        public async Task<UserFlow> CreateFlowAsync(IUser user, string flowName)
        {
            var flowManager = GetService<IFlowManager>();
            var stateSetService = GetService<IStateSetRepository>();

            var stateSet = await stateSetService.GetByNameAsync(flowName);
            var uf = await flowManager.CreateUserFlowAsync(user, stateSet);

            Assert.Equal(uf.Flow.StateName, "edit");
            return uf;
        }

        [Fact]
        public async Task CreateTest()
        {
            var executionService = GetService<IExecutionService>();

            var user = new TestUser();
            var uf = await CreateFlowAsync(user, FlowPreBuilder.FlowName);

            var actions = await executionService.GetTriggerAsync(uf.Flow);
            var trigger = actions[0];
            await executionService.InvokeAsync(user, uf.Flow, trigger);

            actions = await executionService.GetTriggerAsync(uf.Flow);
            trigger = actions[0];
            await executionService.InvokeAsync(user, uf.Flow, trigger);

            actions = await executionService.GetTriggerAsync(uf.Flow);
            trigger = actions[0];
            await executionService.InvokeAsync(user, uf.Flow, trigger);
        }
    }
}
