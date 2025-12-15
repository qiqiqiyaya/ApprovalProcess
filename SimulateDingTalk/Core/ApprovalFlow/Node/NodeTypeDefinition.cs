using Volo.Abp.Data;

namespace Core.ApprovalFlow.Node
{
    /// <summary>
    /// 节点类型定义
    /// </summary>
    public class NodeTypeDefinition : IHasExtraProperties
    {
        public NodeType NodeType { get; set; }

        public GatewayType? GatewayType { get; set; }

        public GatewayDirection? GatewayDirection { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public NodeDefinition Node { get; set; }
    }

    public enum NodeType
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

    public enum GatewayType
    {
        Exclusive, // 排他网关 (XOR)
        Parallel, // 并行网关 (AND)
        Inclusive // 包含网关 (OR)
    }

    // 网关方向
    public enum GatewayDirection
    {
        Split, // 分裂
        Join // 汇聚
    }
}
