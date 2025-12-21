namespace ApFlow.Domain.Models.Enums
{
    /// <summary>
    /// 多人审批时采用的审批方式
    /// </summary>
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
}



