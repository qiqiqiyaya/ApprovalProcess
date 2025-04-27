using Ap.Core;
using Ap.Core.Actions;
using Ap.Core.Actions.Pipeline;
using Ap.Core.Converts;
using Ap.Core.Converts.ToEntity;
using Ap.Core.Converts.ToStateMachines;
using Ap.Core.Converts.ToStateSettings;
using Ap.Core.Converts.ToTransitions;
using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Ap.Repository.FreeSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ap.Register
{
    public static class Register
    {
        public static void AddAp(this IServiceCollection service, Action<ExecutableActionRecord> options)
        {
            //service.AddDbContext<ApprovalProcessDbContext>(options =>
            //{
            //    options.UseInMemoryDatabase("MyDatabase");
            //    options.UseSeeding((dbContext, a) =>
            //    {
            //        if (dbContext is ApprovalProcessDbContext db)
            //        {
            //            SeedData.Initialize(db);
            //        }
            //    });

            //});
            service.AddTransient<IStateMachineLoader, StateMachineLoader>();
            service.AddTransient<IStateMachineActuator, StateMachineActuator>();
            service.AddTransient<IApRepository, ApRepository>();
            service.AddTransient<IPipelineProvider>(serviceProvider =>
            new PipelineProvider(serviceProvider, new Dictionary<string, object>()));

            service.AddConverts();
            service.AddEntryAction(options);
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
            service.AddSingleton(serviceProvider => new ToTransitionContainer(transitionConverters, serviceProvider));


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

            Dictionary<string, ConvertMap> entityConverters = new Dictionary<string, ConvertMap>();
            string key3 = typeof(string).FullName + typeof(string).FullName;
            entityConverters.Add(key3, new ConvertMap
            {
                Key = key3,
                Type = typeof(StringToEntity),
            });
            service.AddSingleton<StringToEntity>();
            service.AddSingleton(serviceProvider => new EntityConvertContainer(entityConverters, serviceProvider));
        }

        private static void AddEntryAction(this IServiceCollection service, Action<ExecutableActionRecord> options)
        {
            var configs = new ExecutableActionRecord(new Dictionary<string, ExecutableActionMap>(),
                new Dictionary<string, ExecutableActionMap>());
            options.Invoke(configs);

            service.AddSingleton(new ExecutableActionContainer(configs.EntryActionConfigs, configs.ExitActionConfigs));
        }
    }
}
