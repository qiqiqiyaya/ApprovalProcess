using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Form
{
    public class FormDefinition : AggregateRoot<Guid>, IFullAuditedObject
    {
        public string Data { get; set; }

        public ICollection<FormField> Fields { get; set; }

        public bool IsDeleted { get; }

        public DateTime? DeletionTime { get; }

        public DateTime CreationTime { get; }

        public DateTime? LastModificationTime { get; }

        public Guid? CreatorId { get; }

        public Guid? LastModifierId { get; }

        public Guid? DeleterId { get; }
    }
}
