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

            foreach (var apCoreOption in options.PreBuilders)
            {
                services.AddTransient(apCoreOption);
            }

            services.AddSingleton(options);
            CreateStateSetBuilderProvider creator =
                (IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null) => new StateSetBuilderProvider(serviceProvider, rootStateLinked);
            services.AddTransient(_ => creator);
            services.AddTransient<IStateSetBuilderProvider, StateSetBuilderProvider>();

            services.AddTransient<IStateSetRepository, MemoryStateSetRepository>();
            services.AddTransient<IFlowService, FlowService>();
            services.AddTransient<IExecutionFlowRepository, MemoryExecutionFlowRepository>();
            services.AddTransient<IExecutionService, ExecutionService>();
            services.AddTransient<IPipelineProvider, PipelineProvider>();


            services.AddTransient<IFlowRecordRepository, MemoryFlowRecordRepository>();
            services.AddTransient<IFlowManager, FlowManager>();
        }
    }
}
