using Ap.Core.Services.Interfaces;
using System.Threading.Tasks;
using Ap.Core.Definitions;
using Ap.Core.Models;

namespace Ap.Core.Services
{
    public class FlowManager : IFlowManager
    {
        private readonly IFlowService _flowService;
        private readonly IExecutionService _executionService;
        private readonly IStateSetRepository _stateSetRepository;

        public FlowManager(IFlowService flowService,
            IExecutionService executionService,
            IStateSetRepository stateSetRepository,
            IUserFlowRepository userFlowRepository)
        {
            _flowService = flowService;
            _executionService = executionService;
            _stateSetRepository = stateSetRepository;
        }

        public async ValueTask<Flow> CreateAsync(IUser user, string rootStateSetId)
        {
            var set = await _stateSetRepository.GetByIdAsync(rootStateSetId);
            var flow = await _flowService.CreateAsync(user, set);
            var triggers = set.GetTrigger();

            await _executionService.InvokeAsync(user, flow, triggers[0], set);
            return await _flowService.GetAsync(flow.Id);
        }

        public async ValueTask<Flow> CreateAsync(IUser user, IStateSet stateSet)
        {
            var flow = await _flowService.CreateAsync(user, stateSet);
            var triggers = stateSet.GetTrigger();

            await _executionService.InvokeAsync(user, flow, triggers[0], stateSet);
            return await _flowService.GetAsync(flow.Id);
        }
    }
}
