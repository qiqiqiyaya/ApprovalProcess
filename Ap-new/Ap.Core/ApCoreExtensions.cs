using Ap.Core.Builders;
using Ap.Core.Definitions;
using Ap.Core.Pipeline;
using Ap.Core.Services;
using Ap.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ap.Core
{
    public static class ApCoreExtensions
    {
        public static void AddApCore(this IServiceCollection services, Action<ApCoreOptions> action)
        {
            var options = new ApCoreOptions();
            action(options);

            foreach (var apCoreOption in options.ApproverConfigurations)
            {
                services.AddTransient(apCoreOption.ApproverServiceType);
                services.AddTransient(apCoreOption.PerBiulderType);
            }

            services.AddSingleton(options);
            CreateStateSetBuilderProvider creator =
                (IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null) => new StateSetBuilderProvider(serviceProvider, rootStateLinked);
            services.AddTransient(_ => creator);
            services.AddTransient<IStateSetBuilderProvider, StateSetBuilderProvider>();

            services.AddTransient<IStateSetService, MemoryStateSetService>();
            services.AddTransient<IFlowService, FlowService>();
            services.AddTransient<IFlowRepository, MemoryFlowRepository>();
            services.AddTransient<IExecutionService, ExecutionService>();
            services.AddTransient<IPipelineProvider, PipelineProvider>();
        }
    }
}
