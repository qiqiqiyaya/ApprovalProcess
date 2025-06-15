using Ap.Core;
using Ap.Core.Builders;
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
                options.AddFlow<FlowPreBuilder>();
            });
            _provider = service.BuildServiceProvider();
            var config = new ApCoreConfigure(_provider);
            config.Configure();
        }

        protected IStateSetBuilderProvider StateSetBuilderProvider => GetService<IStateSetBuilderProvider>();

        protected T GetService<T>() where T : class
        {
            return _provider.GetRequiredService<T>();
        }

    }
}
