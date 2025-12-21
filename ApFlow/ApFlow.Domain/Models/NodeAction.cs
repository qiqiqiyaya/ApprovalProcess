using ApFlow.Domain.Models.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批节点可执行的操作
    /// </summary>
    /// <remarks>
    /// 审批节点操作实体，定义了在特定审批节点上可以执行的操作类型。
    /// 操作类型包括同意、拒绝、转交、撤销等，每种操作都有特定的业务含义和执行逻辑。
    /// 通过节点操作配置，可以灵活控制不同审批节点上可用的操作选项。
    /// </remarks>
    public class NodeAction : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 审批操作类型
        /// </summary>
        /// <remarks>
        /// 定义操作的具体类型，如同意、拒绝、转交、撤销等。
        /// 操作类型决定了该操作的业务含义和执行逻辑。
        /// </remarks>
        public ApprovalActionType ActionType { get; set; }

        /// <summary>
        /// 关联的审批节点标识
        /// </summary>
        /// <remarks>
        /// 引用审批节点的唯一标识，建立操作与节点的关联关系。
        /// 通过此关联可以获取操作所属的节点信息。
        /// </remarks>
        public Guid NodeId { get; set; }

        /// <summary>
        /// 关联的审批节点实体
        /// </summary>
        /// <remarks>
        /// 导航属性，引用关联的审批节点实体，用于访问节点的完整信息。
        /// </remarks>
        public Node Node { get; set; }

        /// <summary>
        /// 扩展属性字典
        /// </summary>
        /// <remarks>
        /// 用于存储操作的扩展属性，如操作名称、描述、自定义配置等。
        /// 扩展属性提供了灵活的方式来存储操作特有的配置信息。
        /// </remarks>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected NodeAction()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的审批节点操作
        /// </summary>
        /// <param name="id">操作唯一标识</param>
        /// <param name="actionType">操作类型</param>
        /// <param name="nodeId">关联的审批节点标识</param>
        /// <remarks>
        /// 初始化一个新的审批节点操作，设置操作类型和关联的节点。
        /// 创建后可以通过扩展属性添加额外的配置信息。
        /// </remarks>
        public NodeAction(
            Guid id,
            ApprovalActionType actionType,
            Guid nodeId)
            : base(id)
        {
            ActionType = actionType;
            NodeId = nodeId;
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }


}



