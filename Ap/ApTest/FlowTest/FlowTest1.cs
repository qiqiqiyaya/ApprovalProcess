using Ap.Core.Models;
using Ap.Core.Services.Interfaces;

namespace ApTest.FlowTest
{
    public class FlowTest1 : Base
    {
        public async Task<Flow> CreateFlowAsync(IUser user, string flowName)
        {
            var flowManager = GetService<IFlowManager>();
            var stateSetService = GetService<IStateSetRepository>();

            var stateSet = await stateSetService.GetByNameAsync(flowName);
            var flow = await flowManager.CreateAsync(user, stateSet);
            return flow;
        }

        [Fact]
        public async Task CreateTest()
        {
            var flowService = GetService<IFlowService>();

            var user = new FlowUser();
            var flow = await CreateFlowAsync(user, FlowPreBuilder.FlowName);

            var actions = await flowService.GetActionsAsync(flow.Id);
            var trigger = actions[0];

            var executionService = GetService<IExecutionService>();
            await executionService.InvokeAsync(user, flow, trigger);
        }
    }
}
