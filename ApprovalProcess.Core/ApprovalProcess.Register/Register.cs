using ApprovalProcess.Core;
using ApprovalProcess.Core.Actions;
using ApprovalProcess.Core.ConvertActions;
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
			service.AddTransient<IStateMachineManager, StateMachineManager>();

			Dictionary<string, ConvertConfiguration> converters = new Dictionary<string, ConvertConfiguration>();
			string key = typeof(string).FullName + typeof(string).FullName;
			converters.Add(key, new ConvertConfiguration
			{
				Key = key,
				Type = typeof(StringToTransition),
			});
			service.AddSingleton(serviceProvider => new ConvertContainer(converters, serviceProvider));

			Dictionary<string, ActionConfiguration> actionConfigs = new Dictionary<string, ActionConfiguration>();
			actionConfigs.Add(SmActionNames.SetNextApprover, new ActionConfiguration
			{
				Key = SmActionNames.SetNextApprover,
				Type = typeof(SetNextApproverAction),
			});
			actionConfigs.Add(SmActionNames.NotificationSend, new ActionConfiguration
			{
				Key = SmActionNames.NotificationSend,
				Type = typeof(NotificationSendAction),
			});
			service.AddSingleton(serviceProvider => new SmActionContainer(actionConfigs, serviceProvider));



		}
	}
}
