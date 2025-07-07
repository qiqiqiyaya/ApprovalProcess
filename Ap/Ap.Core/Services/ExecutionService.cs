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
            var context = new TriggerContext(stateTrigger, flow, user);
            var stateName = GetStateName(flow, set);
            // 恢复状态机状态
            set.Recover(serviceProvider, stateName);

            if (set.IsInitial) await set.InitialEntry(context);

            // 触发
            await set.ExecuteTrigger(context);

            if (set.IsEnd) await set.CompletedExit(context);
        }

        public async ValueTask<StateTriggerCollection> GetTriggerAsync(Flow flow)
        {
            var set = await stateSetRepository.GetByIdAsync(flow.RootStateSetId);
            return await GetTrigger(flow, set);
        }

        protected ValueTask<StateTriggerCollection> GetTrigger(Flow flow, IStateSet stateSet)
        {
            var stateName = GetStateName(flow, stateSet);

            stateSet.Recover(serviceProvider, stateName);
            return stateSet.GetTrigger();
        }

        private string GetStateName(Flow flow, IStateSet stateSet)
        {
            string stateName;
            if (flow.FlowStatus == FlowStatus.Initial)
            {
                stateName = stateSet.InitialState;
            }
            else
            {
                stateName = Flow.GetDeepTriggeredNode(flow).StateName;
            }

            return stateName;
        }
    }
}
