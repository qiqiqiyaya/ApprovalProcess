using Ap.Share.Actions.Entry.NextApprover;
using Microsoft.Extensions.DependencyInjection;
using Sm.Register;
using Sm.Repository.EfSqlserver;

namespace TestProject1
{
    public class BaseTest
    {
        protected readonly IServiceProvider ServiceProvider;
        public BaseTest()
        {
            ServiceCollection service = new ServiceCollection();
            service.AddAp(option =>
            {
                option.AddEntryAction<NextApproverAction, string, string>();
            });
            service.AddSmEfSqlserver();
            ServiceProvider = service.BuildServiceProvider();
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
