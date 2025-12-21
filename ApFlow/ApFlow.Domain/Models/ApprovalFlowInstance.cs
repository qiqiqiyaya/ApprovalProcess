using ApFlow.Domain.Models.Enums;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

// ReSharper disable UnassignedGetOnlyAutoProperty
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批流程实例聚合根，表示一个具体的审批流程执行实例
    /// 继承自ABP框架的AggregateRoot，实现IFullAuditedObject接口以支持完整的审计功能
    /// </summary>
    public class ApprovalFlowInstance : AggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 发起人标识，记录发起该审批流程实例的用户，只能由某一个人发起
        /// </summary>
        public string Starter { get; protected set; }

        /// <summary>
        /// InstanceFormData Id，关联审批流程表单数据的唯一标识
        /// </summary>
        public Guid FormDataId { get; protected set; }

        /// <summary>
        /// 审批流程表单数据，存储该实例提交的表单内容和相关信息
        /// </summary>
        public InstanceFormData FormData { get; protected set; }

        /// <summary>
        /// 关联的审批流程定义标识，指向该实例所遵循的审批流程模板
        /// </summary>
        public Guid ApprovalFlowId { get; protected set; }

        /// <summary>
        /// 审批流程审批记录集合，记录该实例的所有审批操作历史
        /// </summary>
        public ICollection<ApprovalRecord> Records { get; protected set; }

        /// <summary>
        /// 审批流程当前状态，表示该实例在审批流程中所处的阶段
        /// </summary>
        public InstanceStatus Status { get; protected set; }

        /// <summary>
        /// 当前审批节点标识，指向该实例当前所处的审批节点
        /// </summary>
        public Guid CurrentNodeId { get; protected set; }

        /// <summary>
        /// 创建时间，记录审批流程实例的创建时刻
        /// </summary>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 创建者标识，记录创建该审批流程实例的用户
        /// </summary>
        public Guid? CreatorId { get; protected set; }

        /// <summary>
        /// 最后修改时间，记录审批流程实例的最后更新时刻
        /// </summary>
        public DateTime? LastModificationTime { get; protected set; }

        /// <summary>
        /// 最后修改者标识，记录最后修改该审批流程实例的用户
        /// </summary>
        public Guid? LastModifierId { get; protected set; }

        /// <summary>
        /// 软删除标记，标识审批流程实例是否已被删除
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// 删除时间，记录审批流程实例被软删除的时刻
        /// </summary>
        public DateTime? DeletionTime { get; protected set; }

        /// <summary>
        /// 删除者标识，记录执行软删除操作的用户
        /// </summary>
        public Guid? DeleterId { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected ApprovalFlowInstance()
        {

        }

        /// <summary>
        /// 审批流程实例构造函数，用于创建新的审批流程实例
        /// </summary>
        /// <param name="id">审批流程实例唯一标识</param>
        /// <param name="starter">发起人标识</param>
        /// <param name="formData">审批流程表单数据</param>
        /// <param name="approvalFlowId">关联的审批流程定义标识</param>
        public ApprovalFlowInstance(
            Guid id,
            string starter,
            InstanceFormData formData,
            Guid approvalFlowId)
            : base(id)
        {
            Starter = starter;
            FormData = formData;
            ApprovalFlowId = approvalFlowId;
            Status = InstanceStatus.NotSubmitted;
            Records = [];
        }
    }
}


