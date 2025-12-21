using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 流程图基础信息
    /// </summary>
    /// <remarks>
    /// 流程图基础信息实体，存储审批流程图的基本元数据，包括名称、版本、发布状态等。
    /// 这些信息用于流程的识别、版本管理和发布控制。基础信息与流程图主体分离，
    /// 便于流程的版本迭代和信息管理。
    /// </remarks>
    public class GraphBasicInfo : Entity<Guid>
    {
        /// <summary>
        /// 流程图名称
        /// </summary>
        /// <remarks>
        /// 流程图的显示名称，用于用户界面展示和流程识别。
        /// 名称应该简洁明了，能够反映流程的业务用途。
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        /// <remarks>
        /// 流程图的版本标识，用于版本控制和升级管理。
        /// 版本号通常采用语义化版本格式，如"1.0.0"。
        /// </remarks>
        public string Version { get; set; }

        /// <summary>
        /// 是否已发布
        /// </summary>
        /// <remarks>
        /// 标识流程图是否已发布，只有已发布的流程图才能用于创建审批实例。
        /// 未发布的流程图处于编辑状态，可以继续修改和完善。
        /// </remarks>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        /// <remarks>
        /// 记录流程图的发布时间，用于版本管理和审计追踪。
        /// 只有当IsPublished为true时，此字段才有值。
        /// </remarks>
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 流程图描述
        /// </summary>
        /// <remarks>
        /// 流程图的详细说明，描述流程的业务场景、适用范围和使用方法。
        /// 帮助用户理解流程的用途和操作方式。
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// 关联的流程图标识
        /// </summary>
        /// <remarks>
        /// 与主流程图实体的关联标识，用于建立基础信息与流程图主体的关系。
        /// </remarks>
        public Guid GraphId { get; set; }

        /// <summary>
        /// 关联的流程图实体
        /// </summary>
        /// <remarks>
        /// 导航属性，引用关联的流程图实体，用于访问流程图的完整信息。
        /// </remarks>
        public Graph Graph { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected GraphBasicInfo()
        {

        }

        /// <summary>
        /// 创建新的流程图基础信息
        /// </summary>
        /// <param name="id">基础信息唯一标识</param>
        /// <param name="name">流程图名称</param>
        /// <param name="version">版本号</param>
        /// <param name="graphId">关联的流程图标识</param>
        /// <remarks>
        /// 初始化一个新的流程图基础信息实例，设置基本信息并默认为未发布状态。
        /// 创建后可以通过修改IsPublished属性并设置PublishTime来发布流程图。
        /// </remarks>
        public GraphBasicInfo(
            Guid id,
            string name,
            string version,
            Guid graphId)
            : base(id)
        {
            Name = name;
            Version = version;
            GraphId = graphId;
            IsPublished = false;
        }
    }
}


