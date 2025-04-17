using ApprovalProcess.Core;
using ApprovalProcess.Core.Workflows;
using ApprovalProcess.Repository;
using ApprovalProcess.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApprovalProcess.Register
{
    public static class Register
    {
        public static void AddAp(this IServiceCollection service)
        {

            service.AddDbContext<ApprovalProcessDbContext>(options =>
            {
                options.UseInMemoryDatabase("MyDatabase");
                options.UseSeeding((dbContext, a) =>
                {
                    if (dbContext is ApprovalProcessDbContext db)
                    {
                        SeedData.Initialize(db);
                    }
                });

            });

            service.AddTransient<IPeService, PeService>();
            service.AddTransient<IWorkflow, PeWorkflow>();
            service.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
