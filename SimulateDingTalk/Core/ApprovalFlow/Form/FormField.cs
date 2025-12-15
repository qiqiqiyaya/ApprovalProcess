using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Form
{
    public class FormField : Entity<Guid>, IHasExtraProperties
    {
        public string Key { get; set; }

        public string DisplayName { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }
    }
}
