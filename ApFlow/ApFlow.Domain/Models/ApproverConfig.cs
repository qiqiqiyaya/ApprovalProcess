using ApFlow.Domain.Models.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批人配置实体，用于定义审批节点中审批人的配置规则
    /// 继承自ABP框架的Entity，实现IHasExtraProperties接口以支持扩展属性
    /// </summary>
    public class ApproverConfig : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 审批人类型，定义审批人的选择方式（如指定用户、角色、部门等）
        /// </summary>
        public ApproverType Type { get; protected set; }

        /// <summary>
        /// 审批人标识集合，存储具体的审批人标识列表
        /// </summary>
        public ICollection<string> ApproverIds { get; protected set; }

        /// <summary>
        /// 多人审批方式，定义当有多个审批人时的审批策略（如会签、或签等）
        /// </summary>
        public HowApproval ApprovalMode { get; protected set; }

        /// <summary>
        /// 审批人为空时处理方式，定义当找不到审批人时的处理策略
        /// </summary>
        public WhenApproverEmpty EmptyApproverHandling { get; protected set; }

        /// <summary>
        /// 扩展属性字典，用于存储额外的配置信息
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        /// <summary>
        /// 关联的节点标识，指向该审批人配置所属的审批节点
        /// </summary>
        public Guid NodeId { get; protected set; }

        /// <summary>
        /// 关联的节点对象，表示该审批人配置所属的审批节点
        /// </summary>
        public Node Node { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected ApproverConfig()
        {
            ApproverIds = new List<string>();
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的审批人配置
        /// </summary>
        /// <param name="id">审批人配置唯一标识</param>
        /// <param name="type">审批人类型</param>
        /// <param name="approvalMode">多人审批方式</param>
        /// <param name="emptyApproverHandling">审批人为空时处理方式</param>
        /// <param name="nodeId">关联的节点标识</param>
        /// <param name="approverIds">审批人标识集合</param>
        public ApproverConfig(
            Guid id,
            ApproverType type,
            HowApproval approvalMode,
            WhenApproverEmpty emptyApproverHandling,
            Guid nodeId,
            ICollection<string> approverIds)
            : base(id)
        {
            Type = type;
            ApprovalMode = approvalMode;
            EmptyApproverHandling = emptyApproverHandling;
            NodeId = nodeId;
            ApproverIds = approverIds ?? new List<string>();
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}



