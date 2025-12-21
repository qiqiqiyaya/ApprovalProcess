namespace ApFlow.Domain.Models.Enums
{
    /// <summary>
    /// 审批人类型
    /// </summary>
    public enum ApproverType
    {
        /// <summary>
        /// 指定的审批人员
        /// </summary>
        SpecifiedApprover,
        /// <summary>
        /// 指定的角色
        /// </summary>
        SpecifiedRole,
        /// <summary>
        /// 指定的组织机构
        /// </summary>
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
}



