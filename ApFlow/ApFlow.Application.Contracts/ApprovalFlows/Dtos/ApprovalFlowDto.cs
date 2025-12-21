using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace ApFlow.Application.Contracts.ApprovalFlows.Dtos
{
    /// <summary>
    /// 审批流程创建DTO
    /// </summary>
    public class CreateApprovalFlowDto
    {
        /// <summary>
        /// 审批流程名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 审批流程描述
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 审批流程唯一标识
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Identity { get; set; }

        /// <summary>
        /// 关联的表单定义标识
        /// </summary>
        [Required]
        public Guid FormId { get; set; }

        /// <summary>
        /// 关联的审批流程图标识
        /// </summary>
        [Required]
        public Guid GraphId { get; set; }
    }

    /// <summary>
    /// 审批流程更新DTO
    /// </summary>
    public class UpdateApprovalFlowDto
    {
        /// <summary>
        /// 审批流程名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 审批流程描述
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 审批流程唯一标识
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Identity { get; set; }
    }

    /// <summary>
    /// 更新审批流程表单DTO
    /// </summary>
    public class UpdateApprovalFlowFormDto
    {
        /// <summary>
        /// 关联的表单定义标识
        /// </summary>
        [Required]
        public Guid FormId { get; set; }
    }

    /// <summary>
    /// 更新审批流程图DTO
    /// </summary>
    public class UpdateApprovalFlowGraphDto
    {
        /// <summary>
        /// 关联的审批流程图标识
        /// </summary>
        [Required]
        public Guid GraphId { get; set; }
    }

    /// <summary>
    /// 审批流程DTO
    /// </summary>
    public class ApprovalFlowDto : EntityDto<Guid>
    {
        /// <summary>
        /// 审批流程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 审批流程描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 审批流程唯一标识
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 关联的表单定义标识
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// 关联的审批流程图标识
        /// </summary>
        public Guid GraphId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        /// </summary>
        public Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// 审批流程详细信息DTO
    /// </summary>
    public class ApprovalFlowDetailDto : ApprovalFlowDto
    {
        /// <summary>
        /// 关联的表单信息
        /// </summary>
        public FormDto Form { get; set; }

        /// <summary>
        /// 关联的流程图信息
        /// </summary>
        public GraphDto Graph { get; set; }

        /// <summary>
        /// 发起人权限集合
        /// </summary>
        public ICollection<StarterPermissionDto> StarterPermissions { get; set; }
    }

    /// <summary>
    /// 发起人权限DTO
    /// </summary>
    public class StarterPermissionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 权限类型（用户、角色、部门等）
        /// </summary>
        public string PermissionType { get; set; }

        /// <summary>
        /// 权限值（用户ID、角色ID、部门ID等）
        /// </summary>
        public string PermissionValue { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 表单DTO
    /// </summary>
    public class FormDto : EntityDto<Guid>
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表单描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; }
    }

    /// <summary>
    /// 流程图DTO
    /// </summary>
    public class GraphDto : EntityDto<Guid>
    {
        /// <summary>
        /// 流程图名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程图描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 流程图版本
        /// </summary>
        public string Version { get; set; }
    }

    /// <summary>
    /// 获取审批流程列表的输入参数
    /// </summary>
    public class GetApprovalFlowListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 流程名称过滤
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 流程标识过滤
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 表单ID过滤
        /// </summary>
        public Guid? FormId { get; set; }

        /// <summary>
        /// 流程图ID过滤
        /// </summary>
        public Guid? GraphId { get; set; }
    }
}