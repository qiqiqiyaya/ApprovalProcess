using ApFlow.Domain.Models.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 流程连线实体，表示审批流程图中节点之间的连接关系
    /// 继承自ABP框架的Entity，实现IHasExtraProperties接口以支持扩展属性
    /// </summary>
    public class Flow : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 流程方向，定义连线的方向（如正向、反向等）
        /// </summary>
        public FlowDirection Direction { get; protected set; }

        /// <summary>
        /// 条件类型，定义流程流转的条件（如无条件、条件满足等）
        /// </summary>
        public ConditionType Condition { get; protected set; }

        /// <summary>
        /// 源节点标识，表示连线的起始节点
        /// </summary>
        public Guid SourceNodeId { get; protected set; }

        /// <summary>
        /// 目标节点标识，表示连线的目标节点
        /// </summary>
        public Guid TargetNodeId { get; protected set; }

        /// <summary>
        /// 扩展属性字典，用于存储额外的连线配置信息
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        /// <summary>
        /// 关联的流程图标识，指向该连线所属的审批流程图
        /// </summary>
        public Guid GraphId { get; protected set; }

        /// <summary>
        /// 关联的流程图对象，表示该连线所属的审批流程图
        /// </summary>
        public Graph Graph { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected Flow()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的流程连线
        /// </summary>
        /// <param name="id">流程连线唯一标识</param>
        /// <param name="direction">流程方向</param>
        /// <param name="condition">条件类型</param>
        /// <param name="sourceNodeId">源节点标识</param>
        /// <param name="targetNodeId">目标节点标识</param>
        /// <param name="graphId">关联的流程图标识</param>
        public Flow(
            Guid id,
            FlowDirection direction,
            ConditionType condition,
            Guid sourceNodeId,
            Guid targetNodeId,
            Guid graphId)
            : base(id)
        {
            Direction = direction;
            Condition = condition;
            SourceNodeId = sourceNodeId;
            TargetNodeId = targetNodeId;
            GraphId = graphId;
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}



