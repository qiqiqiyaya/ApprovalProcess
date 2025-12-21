namespace ApFlow.Domain.Models.Enums
{
    /// <summary>
    /// 审批人为空时
    /// </summary>
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



