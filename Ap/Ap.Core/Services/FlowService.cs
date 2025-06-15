using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using Ap.Core.Services.Models;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class FlowService : IFlowService
    {
        private readonly IStateSetService _configService;
        private readonly IServiceProvider _provider;
        private readonly IFlowRepository _flowRepository;

        public FlowService(IStateSetService stateSetService,
            IServiceProvider provider,
            IFlowRepository flowRepository)
        {
            _configService = stateSetService;
            _provider = provider;
            _flowRepository = flowRepository;
        }

        public async ValueTask<Flow> GetAsync(string id)
        {
            var flow = await _flowRepository.GetAsync(id);
            return flow;
        }

        public async ValueTask<Flow> CreateAsync(IUser user, IStateSet set)
        {
            var flow = new Flow
            {
                Id = Guid.NewGuid().ToString("N"),
                UserId = user.Id,
                StateSetId = set.Id,
                StateName = set.InitialState,
                CreateTime = DateTime.Now
            };

            //var configuration = await _configService.GetAsync(model.StateSet.Id);
            //var approverService = (_provider.GetRequiredService(configuration.ServiceType) as IApproverService)!;

            //var approverList = await approverService.GetListAsync();
            //flow.Approvers = approverList.Select(s => new NextApprover()
            //{
            //    CreateTime = DateTime.Now,
            //    FlowId = flow.Id,
            //    Id = Guid.NewGuid().ToString("N"),
            //    ObjectId = s
            //}).ToList();

            await _flowRepository.CreateAsync(flow);
            return flow;
        }

        public async ValueTask UpdateAsync()
        {

        }

        public async ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow)
        {
            var set = await _configService.GetByIdAsync(flow.StateSetId);
            set.Recover(flow.StateName);

            return set.GetTrigger();
        }

        public async ValueTask<StateTriggerCollection> GetActionsAsync(string id)
        {
            var flow = await _flowRepository.GetAsync(id);
            return await GetTriggerAsync(flow);
        }
    }
}
