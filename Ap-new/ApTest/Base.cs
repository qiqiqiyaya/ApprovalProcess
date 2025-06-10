using Ap.Core;
using Ap.Core.Builders;
using Ap.Core.Services;
using Ap.Core.Services.Interfaces;
using ApTest.FlowTest;
using Microsoft.Extensions.DependencyInjection;

namespace ApTest
{
    public class Base
    {
        private readonly IServiceProvider _provider;

        public Base()
        {
            var service = new ServiceCollection();
            service.AddApCore(options =>
            {
                options.AddApproverConfig<FlowPreBuilder, FlowApproverService>();
            });
            _provider = service.BuildServiceProvider();
            Configure();
        }

        private void Configure()
        {
            var options = GetService<ApCoreOptions>();
            var builderProvider = GetService<IStateSetBuilderProvider>();
            var approverConfigService = GetService<IStateSetService>();

            // 预构建
            foreach (var configuration in options.ApproverConfigurations)
            {
                var preBuilder = (IPreBuilder)_provider.GetRequiredService(configuration.PerBiulderType);
                var stateSetBuilder = preBuilder.Build(builderProvider);

                var stateSet = stateSetBuilder.Build();
                approverConfigService.Add(new StateSetConfig(configuration, stateSet));
            }
        }

        protected IStateSetBuilderProvider StateSetBuilderProvider => GetService<IStateSetBuilderProvider>();

        protected T GetService<T>() where T : class
        {
            return _provider.GetRequiredService<T>();
        }

    }
}
