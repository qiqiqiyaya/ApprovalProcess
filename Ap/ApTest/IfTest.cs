using Ap.Core.Builders;
using Ap.Core.Models;
using Ap.Core.Services.Interfaces;

namespace ApTest
{
	public class IfFlowPreBuilder : IPreBuilder
	{
		public const string FlowName = "IfFlowFlowTest";

		public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
		{
			var builder = builderProvider.Create("edit");
			builder.Name = FlowName;

			builder.If(context =>
				{
					return true;
				},
					ifBuilderProvider => ifBuilderProvider.Create("aaa"),
				ifBuilderProvider => ifBuilderProvider.Create("bbb"))
				.Then("SecondApprove")
				.Then("ThirdApprove");

			builder.AssignApprover("aaa", context =>
			{
				return new ValueTask<List<string>>(new List<string>() { "11" });
			});
			builder.AssignApprover("bbb", context =>
			{
				return new ValueTask<List<string>>(new List<string>() { "11" });
			});
			builder.AssignApprover("SecondApprove", context =>
			{
				return new ValueTask<List<string>>(new List<string>() { "22" });
			});
			builder.AssignApprover("ThirdApprove", context =>
			{
				return new ValueTask<List<string>>(new List<string>() { "33" });
			});

			return builder;
		}
	}

	public class IfTest : Base
	{
		[Fact]
		public async Task Test1()
		{
			var user = new TestUser();
			var uf = await CreateFlowAsync(user, IfFlowPreBuilder.FlowName);

			await ExecFlow(user, uf.FlowId);
			await ExecFlow(user, uf.FlowId);
			await ExecFlow(user, uf.FlowId);
			await ExecFlow(user, uf.FlowId);

			var recordRepository = GetService<IFlowRecordRepository>();
			var list = await recordRepository.GetListAsync(uf.FlowId);
		}

		public async Task<UserFlow> CreateFlowAsync(IUser user, string flowName)
		{
			var flowManager = GetService<IFlowManager>();
			var stateSetService = GetService<IStateSetRepository>();

			var stateSet = await stateSetService.GetByNameAsync(flowName);
			var uf = await flowManager.CreateUserFlowAsync(user, stateSet);
			return uf;
		}

		private async Task ExecFlow(IUser user, string flowId)
		{
			var flowManager = GetService<IFlowManager>();
			var executionService = GetService<IExecutionService>();
			var flow = await flowManager.GetFlowAsync(flowId);
			var actions = await executionService.GetTriggerAsync(flow);
			var trigger = actions[0];
			await executionService.InvokeAsync(user, flow, trigger);
		}
	}
}
