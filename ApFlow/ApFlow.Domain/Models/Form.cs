using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批流程表单聚合根，定义审批流程中使用的表单结构
    /// 继承自ABP框架的AggregateRoot，实现IFullAuditedObject接口以支持完整的审计功能
    /// </summary>
    public class Form : AggregateRoot<Guid>, IFullAuditedObject
    {
        /// <summary>
        /// 表单UI界面数据，以JSON格式存储表单的布局和样式信息
        /// </summary>
        public string Data { get; protected set; }

        /// <summary>
        /// 表单字段集合，包含该表单的所有字段定义
        /// </summary>
        public ICollection<FormField> Fields { get; protected set; }

        /// <summary>
        /// 软删除标记，标识表单是否已被删除
        /// </summary>
        public bool IsDeleted { get; protected set; }

        /// <summary>
        /// 删除时间，记录表单被软删除的时刻
        /// </summary>
        public DateTime? DeletionTime { get; protected set; }

        /// <summary>
        /// 创建时间，记录表单的创建时刻
        /// </summary>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 最后修改时间，记录表单的最后更新时刻
        /// </summary>
        public DateTime? LastModificationTime { get; protected set; }

        /// <summary>
        /// 创建者标识，记录创建该表单的用户
        /// </summary>
        public Guid? CreatorId { get; protected set; }

        /// <summary>
        /// 最后修改者标识，记录最后修改该表单的用户
        /// </summary>
        public Guid? LastModifierId { get; protected set; }

        /// <summary>
        /// 删除者标识，记录执行软删除操作的用户
        /// </summary>
        public Guid? DeleterId { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected Form()
        {
            Fields = new List<FormField>();
        }

        /// <summary>
        /// 创建新的表单
        /// </summary>
        /// <param name="id">表单唯一标识</param>
        /// <param name="data">表单UI界面数据，JSON格式</param>
        public Form(
            Guid id,
            string data)
            : base(id)
        {
            Data = data;
            Fields = new List<FormField>();
        }
    }
}



