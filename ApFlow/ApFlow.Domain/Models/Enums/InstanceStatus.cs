namespace ApFlow.Domain.Models.Enums
{
    /// <summary>
    /// 审批实例状态
    /// </summary>
    public enum InstanceStatus
    {
        /// <summary>
        /// 未提交 - 审批实例已创建但尚未提交审批
        /// </summary>
        NotSubmitted,

        /// <summary>
        /// 处理中 - 审批实例已提交，正在审批流程中
        /// </summary>
        Processing,

        /// <summary>
        /// 已完成 - 审批实例已完成审批流程（通过或拒绝）
        /// </summary>
        Completed
    }
}
