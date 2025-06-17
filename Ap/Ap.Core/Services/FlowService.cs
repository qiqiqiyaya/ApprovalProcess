using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class FlowService : IFlowService
    {
        private readonly IStateSetService _stateSetService;
        private readonly IExecutionFlowRepository _executionFlowRepository;
        private readonly IFlowRecordRepository _flowRecordRepository;

        public FlowService(IStateSetService stateSetService,
            IExecutionFlowRepository executionFlowRepository,
            IFlowRecordRepository flowRecordRepository)
        {
            _stateSetService = stateSetService;
            _executionFlowRepository = executionFlowRepository;
            _flowRecordRepository = flowRecordRepository;
        }

        public async ValueTask<Flow> GetAsync(string id)
        {
            var flow = await _executionFlowRepository.GetAsync(id);
            return flow;
        }

        public async ValueTask<Flow> CreateAsync(IUser user, string rootStateSetId)
        {
            var set = await _stateSetService.GetByIdAsync(rootStateSetId);
            return await CreateAsync(user, set);
        }

        public async ValueTask<Flow> CreateAsync(IUser user, IStateSet set)
        {
            var flow = new ExecutionFlow()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = set.Id,
                CurrentStateSetId = set.Id,
                StateName = set.CurrentState,
                StateId = set.Id,
                ExecutorId = user.Id,
                CreateTime = DateTime.Now
            };

            await _executionFlowRepository.CreateAsync(flow);
            return flow;
        }

        public async ValueTask UpdateAsync(ExecutionFlow flow)
        {
            await _executionFlowRepository.UpdateAsync(flow);
        }

        public async ValueTask AddRecordAsync(FlowRecord record)
        {
            await _flowRecordRepository.InsertAsync(record);
        }

        public async ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow)
        {
            var set = await _stateSetService.GetByIdAsync(flow.RootStateSetId);
            set.Recover(flow.StateName);

            return set.GetTrigger();
        }

        public async ValueTask<StateTriggerCollection> GetActionsAsync(string id)
        {
            var flow = await _executionFlowRepository.GetAsync(id);
            return await GetTriggerAsync(flow);
        }
    }
}
