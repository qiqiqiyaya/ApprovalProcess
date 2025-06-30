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
        private readonly IStateSetRepository _stateSetRepository;

        public FlowManager(
            IStateSetRepository stateSetRepository,
            IUserFlowRepository userFlowRepository)
        {
            _userFlowRepository = userFlowRepository;
            _stateSetRepository = stateSetRepository;
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
            var node = set.CurrentStateNode;

            var flow = new Flow()
            {
                Id = Guid.NewGuid().ToString("N"),
                RootStateSetId = set.Id,
                StateSetId = set.Id,
                StateName = node.Name,
                StateId = node.Id,
                CreateTime = DateTime.Now,
                FlowStatus = FlowStatus.Initial
            };

            var userFlow = new UserFlow(flow, user);
            userFlow.Id = Guid.NewGuid().ToString("N");
            await _userFlowRepository.CreateAsync(userFlow);
            return userFlow;
        }

        public async ValueTask<UserFlow> GetUserFlow(string flowId)
        {
            return await _userFlowRepository.GetByFlowIdAsync(flowId);
        }

        public async ValueTask UpdateFlowAsync(Flow flow)
        {
            await _userFlowRepository.UpdateAsync(flow);
        }
    }
}
