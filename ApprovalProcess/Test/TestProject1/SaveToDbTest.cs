using Ap.Share.Actions;
using Ap.Share.Actions.Models;
using Sm.Core;
using Sm.Core.Services;
using Sm.Core.StateMachine;
using Sm.Share.Entities;

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

        [Fact]
        public async Task SaveExecutableActionTest()
        {
            var actionService = GetRequiredService<IExecutableActionService>();
            var entity = await actionService.AddAsync("TestAction", "test", ExecutableActionType.Entry);
            Assert.NotNull(entity);
        }

        [Fact]
        public async Task AddTestActionTest()
        {
            var actionService = GetRequiredService<IExecutableActionService>();

            //var existsActions = new List<ActionRecord>()
            //{
            //    new ActionRecord(ExecutableActionNames.TestEntryAction,"test action",ExecutableActionType.Entry),
            //    new ActionRecord(ExecutableActionNames.NotificationSend,"test action",ExecutableActionType.Exit),
            //    new ActionRecord(ExecutableActionNames.SetNextApprover,"test action",ExecutableActionType.Exit)
            //};

            //var names = existsActions.Select(s => s.Name).ToArray();

            //var dic = await actionService.GetListByNameAsync(names);
            //foreach (var action in existsActions)
            //{
            //    if (!dic.ContainsKey(action.Name))
            //    {
            //        await actionService.AddAsync(action.Name, "test action", action.Type);
            //    }
            //}
        }
    }
}
