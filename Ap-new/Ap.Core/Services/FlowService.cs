using Ap.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ap.Core.Services
{
    public class FlowService
    {
        private readonly IApproverConfigService _configService;
        private readonly IServiceProvider _provider;
        private readonly IFlowRepository _flowRepository;

        public FlowService(IApproverConfigService approverConfigService, IServiceProvider provider, IFlowRepository flowRepository)
        {
            _configService = approverConfigService;
            _provider = provider;
            _flowRepository = flowRepository;
        }

        public ValueTask<Flow> Get(string id)
        {
            return new ValueTask<Flow>();
        }

        public async ValueTask<Flow> Create(FlowCreateModel model)
        {
            var flow = new Flow
            {
                Id = Guid.NewGuid().ToString("N"),
                UserId = model.User.Id,
                StateSetId = model.StateSet.Id,
                StateName = model.StateSet.InitialState,
                CreateTime = DateTime.Now
            };

            var configuration = await _configService.GetByStateSetIdAsync(model.StateSet.Id);
            var approverService = (_provider.GetRequiredService(configuration.ServiceType) as IApproverService)!;

            var approverList = await approverService.GetListAsync();
            flow.Approvers = approverList.Select(s => new NextApprover()
            {
                CreateTime = DateTime.Now,
                FlowId = flow.Id,
                Id = Guid.NewGuid().ToString("N"),
                ObjectId = s
            }).ToList();

            await _flowRepository.CreateAsync(flow);
            return flow;
        }
    }
}
