using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace ApFlow.Application.Contracts
{
    /// <summary>
    /// ApFlow应用合约层模块，定义应用服务接口和DTO
    /// </summary>
    [DependsOn(
        typeof(AbpDddApplicationContractsModule)
    )]
    public class ApFlowApplicationContractsModule : AbpModule
    {
    }
}