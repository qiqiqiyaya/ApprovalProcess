using Ap.Core.Definitions;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class ExecutionService(
        IStateSetRepository stateSetRepository,
        IServiceProvider serviceProvider)
        : IExecutionService
    {
        public async ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger)
        {
            // 状态机
            var set = await stateSetRepository.GetByIdAsync(flow.RootStateSetId);
            await InvokeAsync(user, flow, stateTrigger, set);
        }

        public async ValueTask InvokeAsync(IUser user, Flow flow, StateTrigger stateTrigger, IStateSet set)
        {
            // 恢复状态机状态
            set.Recover(flow.StateName);
            var context = new TriggerContext(serviceProvider,
                stateTrigger,
                flow,
                user);

            // 触发
            await set.ExecuteTrigger(context);
        }

        public async ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow)
        {
            var set = await stateSetRepository.GetByIdAsync(flow.RootStateSetId);
            return GetTrigger(flow, set);
        }

        protected StateTriggerCollection GetTrigger(Flow flow, IStateSet stateSet)
        {
            stateSet.Recover(flow.StateName);
            return stateSet.GetTrigger();
        }
    }
}
