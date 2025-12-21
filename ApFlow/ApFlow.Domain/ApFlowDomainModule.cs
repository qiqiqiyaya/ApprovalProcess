using ApFlow.Domain.Share;
using ApFlow.Domain.Services;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ApFlow.Domain
{
    /// <summary>
    /// ApFlow 领域模块，定义领域层相关配置和依赖
    /// </summary>
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(ApFlowDomainShareModule)
    )]
    public class ApFlowDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 注册领域服务
            context.Services.AddTransient<IApprovalFlowDomainService, ApprovalFlowDomainService>();
        }
    }
}