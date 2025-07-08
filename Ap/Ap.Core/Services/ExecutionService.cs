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
        /// <summary>
        /// will to create flow for current user and flow
        /// </summary>
        /// <param name="user"></param>
        /// <param name="flowName"></param>
        /// <returns></returns>
        public async ValueTask<Flow> InvokeAsync(IUser user, string flowName)
        {
            // 状态机
            var context = new TriggerContext(user);
            var set = await stateSetRepository.GetByNameAsync(flowName);

            // It must be in the initialization state
            var entryContext = context.CreateEntryContext();
            entryContext.RootStateSet = set;
            entryContext.CommonConfiguration = set.StateSetConfiguration;
            entryContext.ServiceProvider = serviceProvider;
            entryContext.TriggeredTime = DateTime.UtcNow;

            set.ServiceProvider = serviceProvider;
            await set.Entry(entryContext);

            return entryContext.RootFlow;
        }

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

            // Cannot be in the initial state
            //if (set.IsInitial) throw new Exception("this ");

            // 触发
            await set.ExecuteTrigger(context);

            if (set.IsEnd)
            {
                await set.Exit(context.CreateExitContext());
            }
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
