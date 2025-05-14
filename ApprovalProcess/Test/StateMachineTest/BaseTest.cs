using Microsoft.Extensions.DependencyInjection;

namespace StateMachineTest
{
    public class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {
            ServiceCollection service = new ServiceCollection();
            ServiceProvider = service.BuildServiceProvider();
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
