using ApprovalProcess.Core;
using ApprovalProcess.Core.Actions;
using ApprovalProcess.Core.Converts;
using ApprovalProcess.Core.Converts.ToTransitions;
using ApprovalProcess.Core.Entities;
using ApprovalProcess.Core.Workflows;
using ApprovalProcess.Repository;
using ApprovalProcess.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApprovalProcess.Register
{
	public static class Register
	{
		public static void AddAp(this IServiceCollection service)
		{

			service.AddDbContext<ApprovalProcessDbContext>(options =>
			{
				options.UseInMemoryDatabase("MyDatabase");
				options.UseSeeding((dbContext, a) =>
				{
					if (dbContext is ApprovalProcessDbContext db)
					{
						SeedData.Initialize(db);
					}
				});

			});

			service.AddTransient<IPeService, PeService>();
			service.AddTransient<IWorkflow, PeWorkflow>();
			service.AddTransient<IEmployeeService, EmployeeService>();
			service.AddTransient<IStateMachineLoader, StateMachineLoader>();


			Dictionary<string, ExecutableActionMap> actionConfigs = new Dictionary<string, ExecutableActionMap>();
			actionConfigs.Add(SmActionNames.SetNextApprover, new ExecutableActionMap(SmActionNames.SetNextApprover, typeof(SetNextApproverAction)));
			actionConfigs.Add(SmActionNames.NotificationSend, new ExecutableActionMap(SmActionNames.NotificationSend, typeof(NotificationSendAction)));

			service.AddSingleton(serviceProvider => new ExecutableActionContainer(actionConfigs, serviceProvider));


			service.AddConverts();
		}

		private static void AddConverts(this IServiceCollection service)
		{
			// transition convert
			Dictionary<string, ConvertMap> converters = new Dictionary<string, ConvertMap>();
			string key = typeof(string).FullName + typeof(string).FullName + typeof(TransitionEntity).FullName;
			converters.Add(key, new ConvertMap
			{
				Key = key,
				Type = typeof(EntityToTransitionTransition),
			});
			service.AddSingleton(serviceProvider => new TransitionConContainer(converters, serviceProvider));
		}
	}
}
