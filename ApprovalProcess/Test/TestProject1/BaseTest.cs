using Ap.Core.Actions;
using Ap.Register;
using Ap.Repository.EfSqlserver;
using Microsoft.Extensions.DependencyInjection;
using Test.Common;
using Test.Common.Actions.Entry;
using Test.Common.Actions.Exit;

namespace TestProject1
{
	public class BaseTest
	{
		protected readonly IServiceProvider ServiceProvider;
		public BaseTest()
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
			service.AddEfSqlserver();
			ServiceProvider = service.BuildServiceProvider();
			//var dbContext = GetRequiredService<ApprovalProcessDbContext>();
			//dbContext.Database.EnsureCreated();
		}

		public T GetRequiredService<T>()
		{
			return ServiceProvider.GetRequiredService<T>();
		}
	}
}
