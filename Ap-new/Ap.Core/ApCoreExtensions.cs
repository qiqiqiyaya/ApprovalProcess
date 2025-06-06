using Ap.Core.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Ap.Core
{
    public static class ApCoreExtensions
    {
        public static void AddApCore(this IServiceCollection service)
        {
            service.AddTransient<IStateSetBuilderProvider>(serviceProvider => new StateSetBuilderProvider(serviceProvider));
        }
    }
}
