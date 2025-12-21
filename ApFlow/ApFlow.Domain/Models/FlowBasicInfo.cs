using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 流程基本信息实体，存储审批流程的核心基本信息
    /// 继承自ABP框架的Entity，作为ApprovalFlow聚合根的值对象部分
    /// </summary>
    public class FlowBasicInfo : Entity<Guid>
    {
        /// <summary>
        /// 审批流程名称，用于标识和显示审批流程
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 审批流程描述，详细说明审批流程的用途和规则
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// 审批流程唯一标识，用于系统内部识别和引用审批流程
        /// </summary>
        public string Identity { get; protected set; }

        /// <summary>
        /// 发起人权限集合，定义哪些用户或角色可以发起该审批流程
        /// </summary>
        public ICollection<StarterPermissionReference> StarterPermissions { get; protected set; }

        /// <summary>
        /// 关联的审批流程标识，指向所属的审批流程聚合根
        /// </summary>
        public Guid ApprovalFlowId { get; protected set; }

        /// <summary>
        /// 关联的审批流程对象，表示所属的审批流程聚合根
        /// </summary>
        public ApprovalFlow ApprovalFlow { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected FlowBasicInfo()
        {

        }

        /// <summary>
        /// 创建新的流程基本信息
        /// </summary>
        /// <param name="id">流程基本信息唯一标识</param>
        /// <param name="name">审批流程名称</param>
        /// <param name="description">审批流程描述</param>
        /// <param name="identity">审批流程唯一标识</param>
        /// <param name="approvalFlowId">关联的审批流程标识</param>
        public FlowBasicInfo(
            Guid id,
            string name,
            string description,
            string identity,
            Guid approvalFlowId)
            : base(id)
        {
            Name = name;
            Description = description;
            Identity = identity;
            ApprovalFlowId = approvalFlowId;
            StarterPermissions = new List<StarterPermissionReference>();
        }
    }
}


