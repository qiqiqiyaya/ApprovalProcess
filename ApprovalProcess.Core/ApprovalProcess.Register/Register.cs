using ApprovalProcess.Core;
using ApprovalProcess.Core.Actions;
using ApprovalProcess.Core.Actions.Pipeline;
using ApprovalProcess.Core.Converts;
using ApprovalProcess.Core.Converts.ToStateMachines;
using ApprovalProcess.Core.Converts.ToStateSettings;
using ApprovalProcess.Core.Converts.ToTransitions;
using ApprovalProcess.Core.Entities;
using ApprovalProcess.Core.Repositories;
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

			service.AddTransient<IStateMachineActuator, StateMachineActuator>();
			service.AddTransient<IStateMachineRepository, StateMachineRepository>();
			service.AddTransient<IPipelineProvider>(serviceProvider =>
			new PipelineProvider(serviceProvider, new Dictionary<string, object>()));

			service.AddConverts();
			service.AddExecutableActions();
		}

		private static void AddExecutableActions(this IServiceCollection service)
		{
			Dictionary<string, ExecutableActionMap> exitActionConfigs = new Dictionary<string, ExecutableActionMap>();
			exitActionConfigs.Add(SmActionNames.SetNextApprover, new ExecutableActionMap(SmActionNames.SetNextApprover, typeof(SetNextApproverAction)));
			exitActionConfigs.Add(SmActionNames.NotificationSend, new ExecutableActionMap(SmActionNames.NotificationSend, typeof(NotificationSendAction)));

			Dictionary<string, ExecutableActionMap> entryActionConfigs = new Dictionary<string, ExecutableActionMap>();
			entryActionConfigs.Add(SmActionNames.TestEntryAction, new ExecutableActionMap(SmActionNames.TestEntryAction, typeof(TestEntryAction)));

			service.AddTransient<SetNextApproverAction>();
			service.AddTransient<NotificationSendAction>();
			service.AddTransient<TestEntryAction>();
			service.AddSingleton(serviceProvider => new ExecutableActionContainer(entryActionConfigs, exitActionConfigs));
		}

		private static void AddConverts(this IServiceCollection service)
		{
			// transition convert
			Dictionary<string, ConvertMap> transitionConverters = new Dictionary<string, ConvertMap>();
			string key = typeof(string).FullName + typeof(string).FullName + typeof(TransitionEntity).FullName;
			transitionConverters.Add(key, new ConvertMap
			{
				Key = key,
				Type = typeof(EntityToTransitionTransition),
			});
			service.AddSingleton<EntityToTransitionTransition>();
			service.AddSingleton(serviceProvider => new ToTransitionConContainer(transitionConverters, serviceProvider));


			// transition convert
			Dictionary<string, ConvertMap> stateSettingsConverters = new Dictionary<string, ConvertMap>();
			string key1 = typeof(string).FullName + typeof(string).FullName + typeof(StateSettingsEntity).FullName;
			stateSettingsConverters.Add(key1, new ConvertMap
			{
				Key = key1,
				Type = typeof(EntityToStateSettings),
			});
			service.AddSingleton<EntityToStateSettings>();
			service.AddSingleton(serviceProvider => new ToStateSettingsContainer(stateSettingsConverters, serviceProvider));


			// transition convert
			Dictionary<string, ConvertMap> stateMachineConverters = new Dictionary<string, ConvertMap>();
			string key2 = typeof(string).FullName + typeof(string).FullName + typeof(StateMachineEntity).FullName;
			stateMachineConverters.Add(key2, new ConvertMap
			{
				Key = key2,
				Type = typeof(EntityToStateMachine),
			});
			service.AddSingleton<EntityToStateMachine>();
			service.AddSingleton(serviceProvider => new ToStateMachineContainer(stateMachineConverters, serviceProvider));


		}
	}
}
