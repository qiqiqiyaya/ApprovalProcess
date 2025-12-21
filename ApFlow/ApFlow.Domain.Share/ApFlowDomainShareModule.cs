using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ApFlow.Domain.Share
{
    /// <summary>
    /// ApFlow 领域共享模块，定义领域共享层相关配置和依赖
    /// </summary>
    [DependsOn(
        typeof(AbpDddDomainSharedModule)
    )]
    public class ApFlowDomainShareModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}