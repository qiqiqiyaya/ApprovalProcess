using ApFlow.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace ApFlow.Application
{
    /// <summary>
    /// ApFlow应用层模块，负责业务逻辑处理和领域服务协调
    /// </summary>
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpValidationModule),
        typeof(ApFlowApplicationContractsModule)
    )]
    public class ApFlowApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 配置AutoMapper
            context.Services.AddAutoMapperObjectMapper<ApFlowApplicationModule>();

            // 配置应用服务自动注册
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ApFlowApplicationModule>();
            });

        }
    }
}