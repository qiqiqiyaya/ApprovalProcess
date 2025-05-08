using Ap.Share.Repositories;
using Sm.Core;
using Sm.Core.StateMachine;

namespace TestProject1
{
	public class EmployeeApFlowTest : BaseTest
	{
		[Fact]
		public async Task CreateApFlowTest()
		{
			var loader = GetRequiredService<IStateMachineLoader>();
			var apRepository = GetRequiredService<IApRepository>();

			string id = "a755886439a642fc8202b954ccf9e45e";
			var record = await apRepository.GetLastTriggeredRecordAsync(id);
			StateMachine<string, string> sm;

			if (record != null)
			{
				sm = await loader.GetStateMachineAsync(id, record.CurrentState);
			}
			else
			{
				sm = await loader.GetStateMachineAsync(id);
			}

			await sm.Fire(new FireContext<string, string>(ServiceProvider, "Submitted"));

			Assert.NotNull(sm);
			Assert.NotNull(sm.CurrentState);
			Assert.NotNull(sm.InitialState);
		}
	}
}
