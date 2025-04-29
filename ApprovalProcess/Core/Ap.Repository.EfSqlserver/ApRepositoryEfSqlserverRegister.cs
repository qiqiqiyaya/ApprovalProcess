using Ap.Core.Share.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ap.Repository.EfSqlserver
{
	public static class ApRepositoryEfSqlserverRegister
	{
		public static void AddEfSqlserver(this IServiceCollection service)
		{
			service.AddTransient<IApRepository, ApRepository>();
			service.AddDbContext<ApDbContext>((serviceProvider, options) =>
			{
				var logger = serviceProvider.GetRequiredService<ILogger<ApDbContext>>();
				options.LogTo(msg => logger.LogInformation(msg));
				options.EnableSensitiveDataLogging();
				options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApDb;User Id=sa;Password=123;");
				//options.UseSeeding((dbContext, a) =>
				//{
				//	if (dbContext is ApprovalProcessDbContext db)
				//	{
				//		SeedData.Initialize(db);
				//	}
				//});
				options.EnableDetailedErrors();
			});
		}
	}
}
