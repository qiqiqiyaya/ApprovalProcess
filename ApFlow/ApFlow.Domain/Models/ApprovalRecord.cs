using ApFlow.Domain.Models.Enums;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批记录实体，记录审批流程实例中每个节点的审批操作历史
    /// 继承自ABP框架的Entity，用于跟踪审批过程中的所有操作
    /// </summary>
    public class ApprovalRecord : Entity<Guid>
    {
        /// <summary>
        /// 审批人标识，记录执行审批操作的用户
        /// </summary>
        public string Approver { get; protected set; }

        /// <summary>
        /// 审批意见，记录审批人对该审批项的具体意见或说明
        /// </summary>
        public string Comment { get; protected set; }

        /// <summary>
        /// 审批时间，记录执行审批操作的具体时刻，使用UTC时间
        /// </summary>
        public DateTimeOffset ApprovalTime { get; protected set; }

        /// <summary>
        /// 关联的审批动作标识，指向审批节点上可执行的操作按钮
        /// </summary>
        public Guid NodeActionId { get; protected set; }

        /// <summary>
        /// 审批动作对象，表示该审批记录所执行的具体操作
        /// </summary>
        public NodeAction NodeAction { get; protected set; }

        /// <summary>
        /// 节点状态，表示该审批记录在审批流程中的状态
        /// </summary>
        public ApprovalRecordNodeStatus Status { get; protected set; }

        /// <summary>
        /// 关联的节点定义标识，指向审批流程图中的具体节点
        /// </summary>
        public string NodeDefinitionId { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected ApprovalRecord()
        {
        }

        /// <summary>
        /// 创建新的审批记录
        /// </summary>
        /// <param name="id">审批记录唯一标识</param>
        /// <param name="approver">审批人标识</param>
        /// <param name="nodeActionId">审批动作标识</param>
        /// <param name="nodeDefinitionId">节点定义标识</param>
        /// <param name="status">节点状态</param>
        /// <param name="comment">审批意见</param>
        public ApprovalRecord(
            Guid id,
            string approver,
            Guid nodeActionId,
            string nodeDefinitionId,
            ApprovalRecordNodeStatus status,
            string comment)
            : base(id)
        {
            Approver = approver;
            NodeActionId = nodeActionId;
            NodeDefinitionId = nodeDefinitionId;
            Status = status;
            Comment = comment;
            ApprovalTime = DateTimeOffset.UtcNow;
        }
    }
}



