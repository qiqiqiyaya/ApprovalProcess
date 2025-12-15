using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Node
{
    public class NodeAction : Entity<Guid>, IHasExtraProperties
    {
        public ApprovalActionType ActionType { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public NodeDefinition Node { get; set; }
    }

    public enum ApprovalActionType
    {
        /// <summary>
        /// 通过
        /// </summary>
        Approval,
        Reject,
        /// <summary>
        /// 转办
        /// </summary>
        Transfer,
        /// <summary>
        /// 加签 - 分为前加签和后加签
        /// </summary>
        AddApprovalNode,
        /// <summary>
        /// 退回
        /// </summary>
        Return,
        /// <summary>
        /// 撤销
        /// </summary>
        Revoke,
        /// <summary>
        /// 终止流程
        /// </summary>
        TerminationFlow,
        /// <summary>
        /// 暂存
        /// </summary>
        TemporaryStorage,
        /// <summary>
        /// 提交
        /// </summary>
        Submit,

    }
}
