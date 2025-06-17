using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
	public class ExecutionService : IExecutionService
	{
		private readonly IFlowService _flowService;
		private readonly IStateSetService _stateSetService;
		private readonly IServiceProvider _serviceProvider;

		public ExecutionService(IFlowService flowService,
			IStateSetService stateSetService,
			IServiceProvider serviceProvider)
		{
			_flowService = flowService;
			_stateSetService = stateSetService;
			_serviceProvider = serviceProvider;
		}

		public async ValueTask InvokeAsync(ExecutionParameter parameter)
		{
			// 流程
			var flow = await _flowService.GetAsync(parameter.FlowId);

			// 状态机
			var set = await _stateSetService.GetByIdAsync(flow.StateSetId);

			// 恢复状态机状态
			set.Recover(flow.StateName);

			var context = new TriggerContext(_serviceProvider,
				parameter.StateTrigger,
				flow,
				parameter.User);
			// 触发
			await set.ExecuteTrigger(context);
		}
	}
}
