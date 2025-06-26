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
		public async ValueTask InvokeAsync(IUser user, Node node, StateTrigger stateTrigger)
		{
			// 状态机
			var set = await stateSetRepository.GetByIdAsync(node.RootStateSetId);
			await InvokeAsync(user, node, stateTrigger, set);
		}

		public async ValueTask InvokeAsync(IUser user, Node node, StateTrigger stateTrigger, IStateSet set)
		{
			// 恢复状态机状态
			set.Recover(serviceProvider, node.StateName);

			if (set.IsInitial)
			{
				var initial = new TriggerContext(node, user);
				node.FlowStatus = FlowStatus.Initial;
				await set.InitialEntry(initial);
				node = await initial.FlowRefreshAsync();

			}

			node.FlowStatus = FlowStatus.Running;
			// 触发
			var context = new TriggerContext(stateTrigger, node, user);
			await set.ExecuteTrigger(context);

			if (set.IsEnd)
			{
				node = await context.FlowRefreshAsync();
				var end = new TriggerContext(node, user);
				node.FlowStatus = FlowStatus.Completed;
				await set.CompletedExit(end);
			}
		}

		public async ValueTask<StateTriggerCollection> GetTriggerAsync(Node node)
		{
			var set = await stateSetRepository.GetByIdAsync(node.RootStateSetId);
			return await GetTrigger(node, set);
		}

		protected ValueTask<StateTriggerCollection> GetTrigger(Node node, IStateSet stateSet)
		{
			stateSet.Recover(serviceProvider, node.StateName);
			return stateSet.GetTrigger();
		}
	}
}
