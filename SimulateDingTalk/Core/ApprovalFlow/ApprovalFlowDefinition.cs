using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow
{

    public class ApprovalFlowDefinition : AggregateRoot<Guid>, IFullAuditedObject
    {
        public Guid FormDefinitionId { get; set; }

        public Guid ApprovalFlowGraphId { get; set; }

        public DateTime CreationTime { get; }

        public Guid? CreatorId { get; }

        public DateTime? LastModificationTime { get; }

        public Guid? LastModifierId { get; }

        public bool IsDeleted { get; }

        public DateTime? DeletionTime { get; }

        public Guid? DeleterId { get; }
    }
}
