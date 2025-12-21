using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批流程图
    /// </summary>
    /// <remarks>
    /// 审批流程图是审批工作流的核心模型，包含了审批流程的所有节点和连线信息。
    /// 它定义了审批流程的拓扑结构，包括开始节点、审批节点、决策节点和结束节点，
    /// 以及节点之间的流转关系。通过图结构可以表示复杂的审批流程，包括并行审批、
    /// 条件分支、循环审批等高级场景。
    /// </remarks>
    public class Graph : AggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 流程图基本信息
        /// </summary>
        /// <remarks>
        /// 包含流程图的名称、描述、版本等基本信息，用于流程的识别和管理。
        /// </remarks>
        public GraphBasicInfo BasicInfo { get; protected set; }

        /// <summary>
        /// 审批节点集合
        /// </summary>
        /// <remarks>
        /// 包含流程图中的所有节点，如开始节点、审批节点、决策节点和结束节点。
        /// 每个节点都有其特定的功能和属性，定义了审批流程中的各个步骤。
        /// </remarks>
        public ICollection<Node> Nodes { get; protected set; }

        /// <summary>
        /// 流程连线集合
        /// </summary>
        /// <remarks>
        /// 定义了节点之间的流转关系，包括条件分支、并行分支等。
        /// 连线决定了审批流程的执行路径和流转条件。
        /// </remarks>
        public ICollection<Flow> Flows { get; protected set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        /// <remarks>
        /// 软删除标记，用于标识流程图是否已被删除
        /// </remarks>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        /// <remarks>
        /// 记录流程图的删除时间，用于审计和追踪
        /// </remarks>
        public DateTime? DeletionTime { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <remarks>
        /// 记录流程图的创建时间，用于审计和追踪
        /// </remarks>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        /// <remarks>
        /// 记录流程图的最后修改时间，用于审计和追踪
        /// </remarks>
        public DateTime? LastModificationTime { get; protected set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        /// <remarks>
        /// 记录流程图的创建者标识，用于审计和权限控制
        /// </remarks>
        public Guid? CreatorId { get; protected set; }

        /// <summary>
        /// 最后修改者标识
        /// </summary>
        /// <remarks>
        /// 记录流程图的最后修改者标识，用于审计和权限控制
        /// </remarks>
        public Guid? LastModifierId { get; protected set; }

        /// <summary>
        /// 删除者标识
        /// </summary>
        /// <remarks>
        /// 记录流程图的删除者标识，用于审计和权限控制
        /// </remarks>
        public Guid? DeleterId { get; protected set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected Graph()
        {

        }

        /// <summary>
        /// 创建新的审批流程图
        /// </summary>
        /// <param name="id">流程图唯一标识</param>
        /// <param name="basicInfo">流程图基本信息</param>
        /// <remarks>
        /// 初始化一个新的审批流程图实例，包含基本信息和空的节点、连线集合。
        /// 创建后可以通过添加节点和连线来构建完整的审批流程。
        /// </remarks>
        public Graph(
            Guid id,
            GraphBasicInfo basicInfo)
            : base(id)
        {
            BasicInfo = basicInfo;
            Nodes = new List<Node>();
            Flows = new List<Flow>();
        }
    }
}


