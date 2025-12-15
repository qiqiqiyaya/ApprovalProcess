using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Node
{
    /// <summary>
    /// 审批人定义
    /// </summary>
    public class ApproverDefinition : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 审批人类型
        /// </summary>
        public ApproverType Type { get; set; }

        /// <summary>
        /// 审批人集合
        /// </summary>
        public ICollection<string> ApproverId { get; set; }

        public HowApproval HowApproval { get; set; }

        public WhenApproverEmpty WhenApproverEmpty { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public NodeDefinition Node { get; set; }
    }

    /// <summary>
    /// 审批人类型
    /// </summary>
    public enum ApproverType
    {
        /// <summary>
        /// 指定的审批人员
        /// </summary>
        SpecifiedApprover,
        SpecifiedRole,
        SpecifiedOrganization,
        /// <summary>
        /// By organizational level
        /// </summary>
        ByOrganizationalLevel,
        /// <summary>
        /// 发起人自己
        /// </summary>
        Self,
        /// <summary>
        /// 发起人自自选审批人
        /// </summary>
        SelfSelectedApprover
    }

    public enum HowApproval
    {
        /// <summary>
        /// 或签（只需一名审批人同意或拒绝即可）
        /// </summary>
        AnyOne,
        /// <summary>
        /// 会签（需所有审批人同意，不限顺序）
        /// </summary>
        All
    }

    public enum WhenApproverEmpty
    {
        /// <summary>
        /// 不能发起
        /// </summary>
        CannotContinue,
        /// <summary>
        /// 跳过
        /// </summary>
        Skip,
        /// <summary>
        /// 转交给管理员
        /// </summary>
        SendToAdministrator
    }
}
