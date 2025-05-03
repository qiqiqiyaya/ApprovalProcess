using Ap.Core.Actions;
using Ap.Flow.Share.Actions.NextApprover;
using Ap.Register;
using Ap.Repository.EfSqlserver;
using Microsoft.Extensions.DependencyInjection;
using Test.Common;
using Test.Common.Actions.Entry;
using Test.Common.Actions.Exit;

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
                option.AddEntryAction<SetNextApprover, string, string>();
            });
            service.AddEfSqlserver();
            ServiceProvider = service.BuildServiceProvider();
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
