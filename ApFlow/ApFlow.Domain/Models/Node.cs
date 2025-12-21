using ApFlow.Domain.Models.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批节点
    /// </summary>
    /// <remarks>
    /// 审批节点是审批流程图的基本组成单元，代表审批流程中的一个步骤或决策点。
    /// 节点可以是开始节点、审批节点、决策节点或结束节点，每种类型的节点有不同的功能和行为。
    /// 节点定义了审批人配置、操作权限、表单权限等属性，以及与其他节点的连接关系。
    /// </remarks>
    public class Node : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 审批节点类型
        /// </summary>
        /// <remarks>
        /// 定义节点的类型，如开始节点、审批节点、决策节点、结束节点等。
        /// 不同类型的节点在审批流程中有不同的作用和行为。
        /// </remarks>
        public NodeTypeEnum NodeType { get; set; }

        /// <summary>
        /// 审批人配置
        /// </summary>
        /// <remarks>
        /// 定义该节点的审批人配置，包括审批人的类型、选择方式等。
        /// 审批人配置决定了哪些用户可以处理该节点的审批任务。
        /// </remarks>
        public ApproverConfig Approvers { get; set; }

        /// <summary>
        /// 审批节点操作权限集合
        /// </summary>
        /// <remarks>
        /// 定义在该节点上可以执行的操作，如同意、拒绝、转交、撤销等。
        /// 操作权限控制了审批人在该节点可以进行的操作类型。
        /// </remarks>
        public ICollection<NodeAction> Actions { get; set; }

        /// <summary>
        /// 审批节点表单权限集合
        /// </summary>
        /// <remarks>
        /// 定义在该节点上对表单字段的访问权限，包括只读、可编辑、隐藏等。
        /// 表单权限控制了审批人在该节点可以看到和修改哪些表单字段。
        /// </remarks>
        public ICollection<NodeFormPermissions> FormPermissions { get; set; }

        /// <summary>
        /// 入边集合
        /// </summary>
        /// <remarks>
        /// 指向该节点的连线集合，定义了流程进入该节点的路径。
        /// 入边决定了流程可以从哪些节点流转到当前节点。
        /// </remarks>
        public ICollection<Flow> InFlows { get; set; }

        /// <summary>
        /// 出边集合
        /// </summary>
        /// <remarks>
        /// 从该节点出发的连线集合，定义了流程离开该节点的路径。
        /// 出边决定了流程可以从当前节点流转到哪些后续节点。
        /// </remarks>
        public ICollection<Flow> OutFlows { get; set; }

        /// <summary>
        /// 扩展属性字典
        /// </summary>
        /// <remarks>
        /// 用于存储节点的扩展属性，如节点名称、描述、自定义配置等。
        /// 扩展属性提供了灵活的方式来存储节点特有的配置信息。
        /// </remarks>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected Node()
        {
            Actions = new List<NodeAction>();
            FormPermissions = new List<NodeFormPermissions>();
            InFlows = new List<Flow>();
            OutFlows = new List<Flow>();
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的审批节点
        /// </summary>
        /// <param name="id">节点唯一标识</param>
        /// <param name="nodeType">节点类型</param>
        /// <param name="approvers">审批人配置</param>
        /// <remarks>
        /// 初始化一个新的审批节点，设置节点类型和审批人配置。
        /// 创建后可以通过添加操作权限、表单权限和连线来完善节点的功能。
        /// </remarks>
        public Node(
            Guid id,
            NodeTypeEnum nodeType,
            ApproverConfig approvers)
            : base(id)
        {
            NodeType = nodeType;
            Approvers = approvers;
            Actions = new List<NodeAction>();
            FormPermissions = new List<NodeFormPermissions>();
            InFlows = new List<Flow>();
            OutFlows = new List<Flow>();
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}


