using Ap.Core.Actions.Entry;
using Ap.Core.Actions.Entry.NextApprover;
using Ap.Share.Actions;
using Ap.Share.Services;
using Sm.Core.Services;
using Sm.Share.Entities;
using System.Reflection;
using Xunit.DependencyInjection;
using Xunit.v3;

namespace TestProject1
{
    public class ActionToDbTest(IExecutableActionService actionService, IChangeCurrentUser changeUser)
    {
        [Fact]
        public async Task SaveActionTest()
        {
            await actionService.AddAsync(ExecutableActionNames.OnEntrySetCleanNextApprover,
                "SetCleanNextApprover",
                ExecutableActionType.Entry,
                typeof(CleanNextApprover));

            await actionService.AddAsync(ExecutableActionNames.OnEntrySetNextApprover,
                "SetNextApprover",
                ExecutableActionType.Entry,
                typeof(NextApproverAction));

            await actionService.AddAsync(ExecutableActionNames.OnEntryTriggerRecording,
                "TriggerRecording",
                ExecutableActionType.Entry,
                typeof(TriggerRecording));
        }
    }

    public class AdminUserAttribute(IChangeCurrentUser changeUser) : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest, IXunitTest test)
        {
            changeUser.Default(new Ap.Share.Models.User() { Id = "adminId", Name = "admin" });
        }
    }
}
