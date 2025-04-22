using ApprovalProcess.Register;
using ApprovalProcess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject1
{
    public class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {

            ServiceCollection service = new ServiceCollection();
            service.AddAp();
            ServiceProvider = service.BuildServiceProvider();
            var dbContext = GetRequiredService<ApprovalProcessDbContext>();
            dbContext.Database.EnsureCreated();
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
