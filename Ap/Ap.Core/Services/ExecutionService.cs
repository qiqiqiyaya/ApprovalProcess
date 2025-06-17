using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class ExecutionService(
        IFlowService flowService,
        IStateSetService stateSetService,
        IServiceProvider serviceProvider)
        : IExecutionService
    {
        public async ValueTask InvokeAsync(ExecutionParameter parameter)
        {
            // 流程
            var flow = await flowService.GetAsync(parameter.FlowId);

            // 状态机
            var set = await stateSetService.GetByIdAsync(flow.RootStateSetId);

            // 恢复状态机状态
            set.Recover(flow.StateName);

            var context = new TriggerContext(serviceProvider,
                parameter.StateTrigger,
                flow,
                parameter.User);
            // 触发
            await set.ExecuteTrigger(context);
        }
    }
}
