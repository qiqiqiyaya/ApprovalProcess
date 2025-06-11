using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class ExecutionService : IExecutionService
    {
        private readonly IFlowService _flowService;
        private readonly IStateSetService _stateSetService;

        public ExecutionService(IFlowService flowService, IStateSetService stateSetService)
        {
            _flowService = flowService;
            _stateSetService = stateSetService;
        }

        public async ValueTask InvokeAsync(ExecutionParameter parameter)
        {
            // 流程
            var flow = await _flowService.GetAsync(parameter.FlowId);

            // 状态机
            var set = await _stateSetService.GetByIdAsync(parameter.StateSetId);

            // 恢复状态机状态
            set.Recover(flow.StateName);

            // 触发
            set.ExecuteTrigger(parameter.ChildStateSetId, parameter.Trigger);
        }
    }
}
