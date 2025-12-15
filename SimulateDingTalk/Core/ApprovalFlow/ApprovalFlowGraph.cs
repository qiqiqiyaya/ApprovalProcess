using Core.ApprovalFlow.Flow;
using Core.ApprovalFlow.Graph;
using Core.ApprovalFlow.Node;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow
{
    public class ApprovalFlowGraph : AggregateRoot<Guid>, IFullAuditedObject
    {
        public GraphBasicInfo BasicInfo { get; set; }

        /// <summary>
        /// 审批节点
        /// </summary>
        public ICollection<NodeDefinition> NodeDefinitions { get; set; }

        public ICollection<FlowDefinition> FlowDefinitions { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public bool IsDeleted { get; }

        public DateTime? DeletionTime { get; }

        public DateTime CreationTime { get; }

        public DateTime? LastModificationTime { get; }

        public Guid? CreatorId { get; }

        public Guid? LastModifierId { get; }

        public Guid? DeleterId { get; }
    }
}
