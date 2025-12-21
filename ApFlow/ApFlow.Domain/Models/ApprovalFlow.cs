using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批流程聚合根，表示一个完整的审批流程定义
    /// 继承自ABP框架的AggregateRoot，实现IFullAuditedObject接口以支持完整的审计功能
    /// </summary>
    public class ApprovalFlow : AggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 审批流程基本信息，包含流程名称、描述等核心属性
        /// </summary>
        public FlowBasicInfo BasicInfo { get; protected set; }

        /// <summary>
        /// 关联的表单定义标识，用于指定该审批流程使用的表单模板
        /// </summary>
        public Guid FormId { get; protected set; }

        /// <summary>
        /// 关联的审批流程图标识，用于定义审批节点的流转关系
        /// </summary>
        public Guid GraphId { get; protected set; }

        /// <summary>
        /// 创建时间，记录审批流程定义的创建时刻
        /// </summary>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 创建者标识，记录创建该审批流程的用户
        /// </summary>
        public Guid? CreatorId { get; protected set; }

        /// <summary>
        /// 最后修改时间，记录审批流程定义的最后更新时刻
        /// </summary>
        public DateTime? LastModificationTime { get; protected set; }

        /// <summary>
        /// 最后修改者标识，记录最后修改该审批流程的用户
        /// </summary>
        public Guid? LastModifierId { get; protected set; }

        /// <summary>
        /// 软删除标记，标识审批流程是否已被删除
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// 删除时间，记录审批流程被软删除的时刻
        /// </summary>
        public DateTime? DeletionTime { get; protected set; }

        /// <summary>
        /// 删除者标识，记录执行软删除操作的用户
        /// </summary>
        public Guid? DeleterId { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected ApprovalFlow()
        {

        }

        /// <summary>
        /// 审批流程构造函数，用于创建新的审批流程实例
        /// </summary>
        /// <param name="id">审批流程唯一标识</param>
        /// <param name="basicInfo">审批流程基本信息</param>
        /// <param name="formId">关联的表单定义标识</param>
        /// <param name="graphId">关联的审批流程图标识</param>
        public ApprovalFlow(
            Guid id,
            FlowBasicInfo basicInfo,
            Guid formId,
            Guid graphId)
            : base(id)
        {
            BasicInfo = basicInfo;
            FormId = formId;
            GraphId = graphId;
        }
    }
}



