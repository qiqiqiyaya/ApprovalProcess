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
            var sm = await loader.GetStateMachineAsync("e3e3bab8d75d4989b14cad34575f20b2");

            await sm.Fire(new FireContext<string, string>(ServiceProvider, "Submitted"));

            Assert.NotNull(sm);
            Assert.NotNull(sm.CurrentState);
            Assert.NotNull(sm.InitialState);
        }
    }
}
