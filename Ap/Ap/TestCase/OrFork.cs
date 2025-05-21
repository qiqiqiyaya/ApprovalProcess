using Ap.Flow;

namespace Ap.TestCase
{
	public static class OrFork
	{
		public static void OrForkTest()
		{
			var machine = new StateMachine();

			// 编辑 -> 提交
			machine.Start("Edit")
				.Then("FirstApprove")
				.Then("SecondApprove")
				.Then("ThirdApprove")
				.Complete("Completed");

			machine.Trigger("Submit");

			Console.WriteLine("state: " + machine.CurrentState);
			Console.Read();
		}
	}
}
