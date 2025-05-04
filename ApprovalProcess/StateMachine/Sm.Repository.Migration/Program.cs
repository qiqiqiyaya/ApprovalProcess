using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sm.Register;
using SmRepository.EfSqlserver;

namespace Sm.Repository.Migration
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
                Console.WriteLine("开始迁移");
                await db.Database.MigrateAsync();
                Console.WriteLine("数据库迁移完成");
            }

        }
    }
}
