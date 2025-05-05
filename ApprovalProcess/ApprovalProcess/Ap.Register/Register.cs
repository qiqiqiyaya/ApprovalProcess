using Ap.Core.Actions.Entry;
using Ap.Core.Actions.Entry.NextApprover;
using Ap.Core.Services;
using Ap.Repository.EfSqlserver;
using Ap.Share.Actions.Entry.NextApprover;
using Ap.Share.Repositories;
using Ap.Share.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sm.Register;

namespace Ap.Register
{
    public static class Register
    {
        public static void AddAp(this IServiceCollection service)
        {
            service.AddTransient<IApprovedByOrgService, ApprovedByOrgService>();
            service.AddTransient<IApFlowService, ApFlowService>();
            service.AddTransient<IEmployeeService, EmployeeService>();
            service.AddTransient<IOrganizationManager, OrganizationManager>();
            service.AddTransient<IApRepository, ApRepository>();

            service.AddDbContext<ApDbContext>((serviceProvider, options) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<ApDbContext>>();
                options.LogTo(msg => logger.LogInformation(msg));
                options.EnableSensitiveDataLogging();
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
                options.EnableDetailedErrors();
            });

            service.AddSm(option =>
            {
                option.AddEntryAction<NextApproverAction, string, string>();
                option.AddEntryAction<CleanNextApprover, string, string>();
            });
        }
    }
}
