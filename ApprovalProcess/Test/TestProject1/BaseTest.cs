using Ap.Register;
using Microsoft.Extensions.DependencyInjection;
using Sm.Repository.EfSqlserver;

namespace TestProject1
{
    public class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {
            ServiceCollection service = new ServiceCollection();
            service.AddAp();
            service.AddSmSqlserverRepository();
            ServiceProvider = service.BuildServiceProvider();
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
