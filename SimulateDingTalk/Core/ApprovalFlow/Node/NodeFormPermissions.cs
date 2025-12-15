using Core.ApprovalFlow.Form;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Node
{
    /// <summary>
    /// 表单权限
    /// </summary>
    public class NodeFormPermissions : Entity<Guid>, IHasExtraProperties
    {
        public FormFieldPermissions FieldPermissions { get; set; }

        public Guid FormFieldId { get; set; }

        public FormField FormField { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }
    }
}
