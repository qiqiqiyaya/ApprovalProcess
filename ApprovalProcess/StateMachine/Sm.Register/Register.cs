using Microsoft.Extensions.DependencyInjection;
using Sm.Core;
using Sm.Core.Actions;
using Sm.Core.Actions.Pipeline;
using Sm.Core.Converts;
using Sm.Core.Converts.ToEntity;
using Sm.Core.Converts.ToStateMachines;
using Sm.Core.Converts.ToStateSettings;
using Sm.Core.Converts.ToTransitions;
using Sm.Core.Services;
using Sm.Share.Entities;

namespace Sm.Register
{
    public static class Register
    {
        public static void AddAp(this IServiceCollection service, Action<ApRegisterOption>? optionAction = null)
        {
            var options = new ApRegisterOption();
            optionAction?.Invoke(options);

            service.AddTransient<IStateMachineLoader, StateMachineLoader>();
            service.AddTransient<IStateMachineService, StateMachineService>();
            service.AddTransient<IStateMachineActuator, StateMachineActuator>();
            service.AddTransient<IExecutableActionService, ExecutableActionService>();

            service.AddTransient<IPipelineProvider>(serviceProvider =>
            new PipelineProvider(serviceProvider, new Dictionary<string, object>()));

            service.AddSingleton(new ExecutableActionContainer(options.EntryActions, options.ExitActions));
            service.AddConverts();
            service.AddLogging();
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

    }
}
