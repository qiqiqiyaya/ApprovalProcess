using Ap.Core.Actions;
using Ap.Register;
using Ap.Repository.EfSqlserver;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ap.Repository.Migration
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			ServiceCollection service = new ServiceCollection();
			service.AddAp();
			service.AddEfSqlserver();
			var serviceProvider = service.BuildServiceProvider();

			using (var scope = serviceProvider.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<ApDbContext>();
				await db.Database.MigrateAsync();
			}

		}
	}
}
