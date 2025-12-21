using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ApFlow.Domain.Services
{
    /// <summary>
    /// 审批流程领域服务接口，定义审批流程相关的核心业务逻辑
    /// </summary>
    public interface IApprovalFlowDomainService : IDomainService
    {
        /// <summary>
        /// 创建新的审批流程
        /// </summary>
        /// <param name="name">审批流程名称</param>
        /// <param name="description">审批流程描述</param>
        /// <param name="identity">审批流程唯一标识</param>
        /// <param name="formId">关联的表单定义标识</param>
        /// <param name="graphId">关联的审批流程图标识</param>
        /// <returns>创建的审批流程实体</returns>
        Task<ApprovalFlow> CreateAsync(
            string name,
            string description,
            string identity,
            Guid formId,
            Guid graphId);

        /// <summary>
        /// 更新审批流程基本信息
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <param name="name">新的审批流程名称</param>
        /// <param name="description">新的审批流程描述</param>
        /// <param name="identity">新的审批流程唯一标识</param>
        /// <returns>更新后的审批流程实体</returns>
        Task<ApprovalFlow> UpdateBasicInfoAsync(
            ApprovalFlow approvalFlow,
            string name,
            string description,
            string identity);

        /// <summary>
        /// 更新审批流程关联的表单
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <param name="formId">新的表单定义标识</param>
        /// <returns>更新后的审批流程实体</returns>
        Task<ApprovalFlow> UpdateFormAsync(
            ApprovalFlow approvalFlow,
            Guid formId);

        /// <summary>
        /// 更新审批流程关联的流程图
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <param name="graphId">新的流程图标识</param>
        /// <returns>更新后的审批流程实体</returns>
        Task<ApprovalFlow> UpdateGraphAsync(
            ApprovalFlow approvalFlow,
            Guid graphId);

        /// <summary>
        /// 验证审批流程是否可以删除
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <returns>是否可以删除</returns>
        Task<bool> CanDeleteAsync(ApprovalFlow approvalFlow);

        /// <summary>
        /// 验证审批流程定义是否完整有效
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <returns>验证结果</returns>
        Task<bool> ValidateAsync(ApprovalFlow approvalFlow);

        /// <summary>
        /// 获取审批流程的发起人权限
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <returns>发起人权限集合</returns>
        Task<ICollection<StarterPermissionReference>> GetStarterPermissionsAsync(ApprovalFlow approvalFlow);

        /// <summary>
        /// 添加审批流程的发起人权限
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <param name="starterPermission">发起人权限引用</param>
        /// <returns>任务</returns>
        Task AddStarterPermissionAsync(
            ApprovalFlow approvalFlow,
            StarterPermissionReference starterPermission);

        /// <summary>
        /// 移除审批流程的发起人权限
        /// </summary>
        /// <param name="approvalFlow">审批流程实体</param>
        /// <param name="starterPermissionId">发起人权限标识</param>
        /// <returns>任务</returns>
        Task RemoveStarterPermissionAsync(
            ApprovalFlow approvalFlow,
            Guid starterPermissionId);
    }
}