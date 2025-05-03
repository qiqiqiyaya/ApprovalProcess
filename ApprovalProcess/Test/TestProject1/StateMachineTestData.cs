using Ap.Core.StateMachine;

namespace TestProject1
{
	public class StateMachineTestData
	{
		/// <summary>
		/// 2级审批流程
		/// </summary>
		public static StateMachine<string, string> TwoLevelApprovalProcess()
		{
			var machine = new StateMachine<string, string>("Edit");

			// 编辑 -> 提交
			machine.Configure("Edit")
				.Permit("Submitted", "FirstApprove");

			// 退回后，重写 -> 编辑状态
			machine.Configure("Return")
				.Permit("Rewrite", "Edit");

			// 第一级审批
			machine.Configure("FirstApprove")
				.OnEntry("TestEntryAction")
				.Permit("FirstApprovedPass", "SecondApprove")
				.Permit("Reject", "Return");

			// 第二级审批
			machine.Configure("SecondApprove")
				.OnEntry("TestEntryAction")
				.Permit("SecondApprovedPass", "Completed")
				.Permit("Reject", "Return");

			return machine;
		}
	}
}
