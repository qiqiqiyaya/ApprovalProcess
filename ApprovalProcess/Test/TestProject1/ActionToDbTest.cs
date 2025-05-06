using Ap.Core.Actions.Entry;
using Ap.Core.Actions.Entry.NextApprover;
using Ap.Share.Actions;
using Sm.Core.Services;
using Sm.Share.Entities;

namespace TestProject1
{
    public class ActionToDbTest : BaseTest
    {
        [Fact]
        public async Task SaveActionTest()
        {
            var actionService = GetRequiredService<IExecutableActionService>();
            await actionService.AddAsync(ExecutableActionNames.OnEntrySetCleanNextApprover,
                "SetCleanNextApprover",
                ExecutableActionType.Entry,
                typeof(CleanNextApprover));

            await actionService.AddAsync(ExecutableActionNames.OnEntrySetNextApprover,
                "SetNextApprover",
                ExecutableActionType.Entry,
                typeof(NextApproverAction));
        }
    }
}
