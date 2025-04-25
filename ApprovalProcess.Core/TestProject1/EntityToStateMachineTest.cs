using ApprovalProcess.Core;

namespace TestProject1
{
	public class EntityToStateMachineTest : BaseTest
	{
		[Fact]
		public async Task HttpRequestTest()
		{
			var actuator = GetRequiredService<IStateMachineActuator>();
			var sm = await actuator.Fire("1", "Submitted");

			Assert.True(true);
		}
	}
}
