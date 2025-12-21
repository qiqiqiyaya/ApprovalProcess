using ApFlow.EntityFrameworkCore.Sqlserver.DbContexts;
using ApFlow.EntityFrameworkCore.Sqlserver.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace ApFlow.EntityFrameworkCore.Sqlserver
{
    /// <summary>
    /// 基础设施模块
    /// </summary>
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class ApFlowEntityFrameworkCoreSqlserverModule : AbpModule
    {
        /// <summary>
        /// 配置服务依赖注入
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 配置EF Core DbContext
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            // 注册DbContext
            context.Services.AddAbpDbContext<ApFlowDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            // 注册自定义仓储实现
            context.Services.AddTransient<IApprovalFlowRepository, ApprovalFlowRepository>();
            context.Services.AddTransient<IGraphRepository, GraphRepository>();
            context.Services.AddTransient<IFormRepository, FormRepository>();
            context.Services.AddTransient<INodeRepository, NodeRepository>();
            context.Services.AddTransient<IFlowRepository, FlowRepository>();
        }
    }
}