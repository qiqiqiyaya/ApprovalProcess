using Ap.Core.Builders;
using Ap.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ap.Core
{
    public class ApCoreConfigure(IServiceProvider provider)
    {
        public void Configure()
        {
            using (var scope = provider.CreateScope())
            {
                var newProvider = scope.ServiceProvider;

                var options = newProvider.GetRequiredService<ApCoreOptions>();
                var builderProvider = newProvider.GetRequiredService<IStateSetBuilderProvider>();
                var stateSetService = newProvider.GetRequiredService<IStateSetRepository>();

                // pre build
                foreach (var type in options.PreBuilders)
                {
                    var preBuilder = (IPreBuilder)newProvider.GetRequiredService(type);
                    var stateSetBuilder = preBuilder.Build(builderProvider);
                    stateSetService.Add(stateSetBuilder.Build());
                }
            }
        }
    }
}
