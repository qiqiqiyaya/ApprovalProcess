using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sm.Core;
using Sm.Core.Actions;
using Sm.Core.Services;
using Sm.Core.StateMachine;
using Sm.Register;
using Test.Common;
using Test.Common.Actions.Entry;
using Test.Common.Actions.Exit;

namespace ConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            service.AddAp(record =>
            {
                record.EntryActionConfigs.Add(ExecutableActionNames.TestEntryAction,
                    new ExecutableActionMap(ExecutableActionNames.TestEntryAction, typeof(TestEntryAction)));

                record.ExitActionConfigs.Add(ExecutableActionNames.NotificationSend,
                    new ExecutableActionMap(ExecutableActionNames.NotificationSend, typeof(NotificationSendAction)));
                record.ExitActionConfigs.Add(ExecutableActionNames.SetNextApprover,
                    new ExecutableActionMap(ExecutableActionNames.SetNextApprover, typeof(SetNextApproverAction)));
            });

            service.AddLogging(configure => configure.AddConsole());

            var serviceProvider = service.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting application...");

            var stateMachineService = serviceProvider.GetRequiredService<IStateMachineService>();
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
            var stateMachineLoader = serviceProvider.GetRequiredService<IStateMachineLoader>();
            var sm = await stateMachineLoader.GetStateMachine(entity.Id);

            Console.Read();
        }
    }
}
