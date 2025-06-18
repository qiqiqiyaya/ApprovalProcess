using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class FlowService : IFlowService
    {
        private readonly IStateSetRepository _stateSetRepository;
        private readonly IExecutionFlowRepository _executionFlowRepository;
        private readonly IFlowRecordRepository _flowRecordRepository;
        private readonly IServiceProvider _serviceProvider;

        public FlowService(IStateSetRepository stateSetRepository,
            IExecutionFlowRepository executionFlowRepository,
            IFlowRecordRepository flowRecordRepository,
            IServiceProvider serviceProvider)
        {
            _stateSetRepository = stateSetRepository;
            _executionFlowRepository = executionFlowRepository;
            _flowRecordRepository = flowRecordRepository;
            _serviceProvider = serviceProvider;
        }

        public async ValueTask<Flow> GetAsync(string id)
        {
            var flow = await _executionFlowRepository.GetAsync(id);
            return flow;
        }

        public async ValueTask<Flow> CreateAsync(IUser user, string rootStateSetId)
        {
            var set = await _stateSetRepository.GetByIdAsync(rootStateSetId);
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

        public async ValueTask<StateTriggerCollection> GetActionsAsync(string id)
        {
            var flow = await _executionFlowRepository.GetAsync(id);
            return await GetTriggerAsync(flow);
        }

        public async ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow)
        {
            var set = await _stateSetRepository.GetByIdAsync(flow.RootStateSetId);
            return GetTrigger(flow, set);
        }

        protected StateTriggerCollection GetTrigger(Flow flow, IStateSet stateSet)
        {
            stateSet.Recover(flow.StateName);
            return stateSet.GetTrigger();
        }
    }
}
