using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ApFlow.Application.Contracts.ApprovalFlows
{
    using Dtos;

    /// <summary>
    /// 审批流程应用服务接口
    /// </summary>
    public interface IApprovalFlowAppService : ICrudAppService<
        ApprovalFlowDto,
        Guid,
        GetApprovalFlowListDto,
        CreateApprovalFlowDto,
        UpdateApprovalFlowDto>
    {
        /// <summary>
        /// 获取审批流程详细信息
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <returns>审批流程详细信息</returns>
        Task<ApprovalFlowDetailDto> GetDetailAsync(Guid id);

        /// <summary>
        /// 更新审批流程关联的表单
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <param name="input">更新表单输入参数</param>
        /// <returns>更新后的审批流程DTO</returns>
        Task<ApprovalFlowDto> UpdateFormAsync(Guid id, UpdateApprovalFlowFormDto input);

        /// <summary>
        /// 更新审批流程关联的流程图
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <param name="input">更新流程图输入参数</param>
        /// <returns>更新后的审批流程DTO</returns>
        Task<ApprovalFlowDto> UpdateGraphAsync(Guid id, UpdateApprovalFlowGraphDto input);

        /// <summary>
        /// 验证审批流程定义是否完整有效
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <returns>验证结果</returns>
        Task<bool> ValidateAsync(Guid id);

        /// <summary>
        /// 获取审批流程的发起人权限
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <returns>发起人权限集合</returns>
        Task<List<StarterPermissionDto>> GetStarterPermissionsAsync(Guid id);

        /// <summary>
        /// 添加审批流程的发起人权限
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <param name="input">发起人权限DTO</param>
        /// <returns>更新后的审批流程DTO</returns>
        Task<ApprovalFlowDto> AddStarterPermissionAsync(Guid id, StarterPermissionDto input);

        /// <summary>
        /// 移除审批流程的发起人权限
        /// </summary>
        /// <param name="id">审批流程ID</param>
        /// <param name="permissionId">发起人权限ID</param>
        /// <returns>更新后的审批流程DTO</returns>
        Task<ApprovalFlowDto> RemoveStarterPermissionAsync(Guid id, Guid permissionId);

        /// <summary>
        /// 获取可用的表单列表
        /// </summary>
        /// <returns>表单列表</returns>
        Task<List<FormDto>> GetAvailableFormsAsync();

        /// <summary>
        /// 获取可用的流程图列表
        /// </summary>
        /// <returns>流程图列表</returns>
        Task<List<GraphDto>> GetAvailableGraphsAsync();
    }
}