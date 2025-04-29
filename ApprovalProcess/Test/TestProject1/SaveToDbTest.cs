using Ap.Core;
using Ap.Core.Services;
using Ap.Core.StateMachine;

namespace TestProject1
{
	public class SaveToDbTest : BaseTest
	{
		[Fact]
		public async Task SaveStateMachineTest()
		{
			var stateMachineService = GetRequiredService<IStateMachineService>();

			var machine = new StateMachine<string, string>("Edit");

			// 编辑 -> 提交
			machine.Configure("Edit")
				.Permit("Submitted", "FirstApprove");

			// 退回后，重写 -> 编辑状态
			machine.Configure("Return")
				.Permit("Rewrite", "Edit");

			// 第一级审批
			machine.Configure("FirstApprove")
				.Permit("FirstApprovedPass", "SecondApprove")
				.Permit("Reject", "Return");

			// 第二级审批
			machine.Configure("SecondApprove")
				.Permit("SecondApprovedPass", "Completed")
				.Permit("Reject", "Return");

			var entity = await stateMachineService.SaveAsync(machine);
			var stateMachineLoader = GetRequiredService<IStateMachineLoader>();
			var sm = await stateMachineLoader.GetStateMachine(entity.Id);
			Assert.NotNull(sm);
		}
	}
}
