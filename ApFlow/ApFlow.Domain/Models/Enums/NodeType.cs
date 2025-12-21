namespace ApFlow.Domain.Models.Enums
{
    public enum NodeTypeEnum
    {
        Approval, // 审批节点
        Gateway, // 网关节点
        Start, // 开始节点
        End, // 结束节点
        /// <summary>
        /// 抄送
        /// </summary>
        Cc
    }
}



