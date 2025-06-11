using Ap.Core.Services;
using Ap.Core.Services.Interfaces;

namespace ApTest.FlowTest
{
    public class FlowTest1 : Base
    {
        public async Task<Flow> CreateFlowAsync(IUser user, string flowName)
        {
            var flowService = GetService<IFlowService>();
            var stateSetService = GetService<IStateSetService>();

            var stateSet = await stateSetService.GetByNameAsync(flowName);
            var flow = await flowService.CreateAsync(user, stateSet);

            return flow;
        }

        [Fact]
        public async Task CreateTest()
        {
            var flowService = GetService<IFlowService>();

            var user = new FlowUser();
            var flow = await CreateFlowAsync(user, FlowPreBuilder.FlowName);

            var triggerList = await flowService.GetTriggerAsync(flow.Id);
            var trigger = triggerList[0];

            var executionService = GetService<IExecutionService>();
            await executionService.InvokeAsync(new ExecutionParameter()
            {
                FlowId = flow.Id,
                StateSetId = flow.StateSetId,
                Trigger = trigger.Trigger,
                User = user,
            });
        }
    }
}
