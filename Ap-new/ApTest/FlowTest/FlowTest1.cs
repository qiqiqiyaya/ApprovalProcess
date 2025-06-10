using Ap.Core.Services;
using Ap.Core.Services.Interfaces;

namespace ApTest.FlowTest
{
    public class FlowTest1 : Base
    {
        [Fact]
        public async Task CreateTest()
        {
            var flowService = GetService<IFlowService>();
            var stateSetService = GetService<IStateSetService>();

            var config = await stateSetService.GetByNameAsync("FlowTest");
            var user = new FlowUser();
            var flow = await flowService.CreateAsync(user, config.StateSet);


            var executionService = GetService<IExecutionService>();
            await executionService.InvokeAsync(new ExecutionParameter()
            {
                FlowId = flow.Id,
                StateSetId = flow.StateSetId,
                Trigger = "",
                User = user,
            });


        }
    }
}
