using Ap.Core.Actions;
using Ap.Register;
using Microsoft.Extensions.DependencyInjection;
using Test.Common;
using Test.Common.Actions.Entry;
using Test.Common.Actions.Exit;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
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

            var serviceProvider = service.BuildServiceProvider();


            //serviceProvider.GetRequiredService<>()
        }
    }
}
