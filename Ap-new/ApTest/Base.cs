using Ap.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ApTest
{
    public class Base
    {
        protected IServiceProvider ServiceProvider;

        public Base()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddApCore();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>() where T : notnull
        {
            var service = ServiceProvider.GetRequiredService<T>();
            return service;
        }
    }
}
