using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class FlowManager : IFlowManager
    {
        private readonly IUserFlowRepository _userFlowRepository;
        private readonly IExecutionService _executionService;
        private readonly IStateSetRepository _stateSetRepository;
        private readonly IFlowRecordRepository _flowRecordRepository;

        public FlowManager(
            IExecutionService executionService,
            IStateSetRepository stateSetRepository,
            IUserFlowRepository userFlowRepository,
            IFlowRecordRepository flowRecordRepository)
        {
            _executionService = executionService;
            _userFlowRepository = userFlowRepository;
            _stateSetRepository = stateSetRepository;
            _flowRecordRepository = flowRecordRepository;
        }

        public async ValueTask<Flow> GetFlowAsync(string flowId)
        {
            var flow = await _userFlowRepository.GetFlowAsync(flowId);
            return flow;
        }

        public async ValueTask<UserFlow> CreateUserFlowAsync(IUser user, string rootStateSetId)
        {
            var set = await _stateSetRepository.GetByIdAsync(rootStateSetId);
            return await CreateUserFlowAsync(user, set);
        }

        public async ValueTask<UserFlow> CreateUserFlowAsync(IUser user, IStateSet set)
        {
            var flow = new Flow()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = set.Id,
                StateSetId = set.Id,
                StateName = set.CurrentState,
                StateId = set.Id,
                ExecutorId = user.Id,
                CreateTime = DateTime.Now
            };

            var userFlow = new UserFlow(flow, user);
            await _userFlowRepository.CreateAsync(userFlow);

            var triggers = await _executionService.GetTriggerAsync(flow);
            flow.FlowStatus = FlowStatus.Start;
            await _executionService.InvokeAsync(user, flow, triggers[0]);

            return userFlow;
        }

        public async ValueTask UpdateFlowAsync(Flow flow)
        {
            await _userFlowRepository.UpdateAsync(flow);
        }

        public async ValueTask AddRecordAsync(FlowRecord record)
        {
            await _flowRecordRepository.InsertAsync(record);
        }


        //public async ValueTask<Flow> CreateAsync(IUser user, IStateSet stateSet)
        //{
        //    var flow = await _flowService.CreateAsync(user, stateSet);
        //    var triggers = stateSet.GetTrigger();

        //    await _executionService.InvokeAsync(user, flow, triggers[0], stateSet);
        //    return await _flowService.GetAsync(flow.Id);
        //}
    }
}
