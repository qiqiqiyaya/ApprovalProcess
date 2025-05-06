using Sm.Core;

namespace TestProject1
{
    public class EmployeeApFlowTest : BaseTest
    {
        [Fact]
        public async Task CreateApFlowTest()
        {
            var loader = GetRequiredService<IStateMachineLoader>();
            var sm = await loader.GetStateMachineAsync("45d6b155941a4e5189830a48839904c8");


            Assert.NotNull(sm);
        }
    }
}
