using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sm.Share.Repositories;

namespace Sm.Repository.EfSqlserver
{
    public static class SmRepositoryEfSqlserverRegister
    {
        public static void AddSmSqlserverRepository(this IServiceCollection service)
        {
            service.AddTransient<ISmRepository, SmRepository>();
            service.AddDbContext<SmDbContext>((serviceProvider, options) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SmDbContext>>();
                options.LogTo(msg => logger.LogInformation(msg));
                options.EnableSensitiveDataLogging();
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
                options.EnableDetailedErrors();
            });
        }
    }
}
