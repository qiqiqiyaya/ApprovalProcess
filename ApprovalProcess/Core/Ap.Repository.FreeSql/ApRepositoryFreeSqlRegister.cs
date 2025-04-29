using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ap.Repository.FreeSql
{
	public static class ApRepositoryFreeSqlRegister
	{
		public static void AddApRepositoryFreeSql(this IServiceCollection service)
		{
			service.AddSingleton<IFreeSql>(serviceProvider =>
			{
				var logger = serviceProvider.GetRequiredService<ILogger<IFreeSql>>();
				IFreeSql fsql = new FreeSqlBuilder()
					.UseConnectionString(DataType.Sqlite, @"Data Source=Ap.db")
					.UseAdoConnectionPool(true)
					.UseMonitorCommand(cmd => logger.LogDebug($"Sql：{cmd.CommandText}"))
					.UseAutoSyncStructure(true) //自动同步实体结构到数据库，只有CRUD时才会生成表
					.Build();

				fsql.SetDbContextOptions(opt =>
				{
					opt.EnableCascadeSave = true; //级联保存
				});
				return fsql;
			});
		}
	}
}
