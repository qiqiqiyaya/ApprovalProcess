using Ap.Register;
using Ap.Repository.EfSqlserver;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sm.Register;
using Sm.Repository.EfSqlserver;

namespace EfCore.Migration
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            service.AddAp();
            service.AddSmSqlserverRepository();
            var serviceProvider = service.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApDbContext>();
                Console.WriteLine("开始迁移");
                await db.Database.MigrateAsync();
                SeedData.Initialize(db);

                Console.WriteLine("数据库迁移完成");
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SmDbContext>();
                Console.WriteLine("开始迁移");
                await db.Database.MigrateAsync();

                Console.WriteLine("数据库迁移完成");
            }
        }
    }
}
