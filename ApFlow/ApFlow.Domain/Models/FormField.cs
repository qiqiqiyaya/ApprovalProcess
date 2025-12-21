using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批流程表单字段实体，定义表单中的单个字段属性
    /// 继承自ABP框架的Entity，实现IHasExtraProperties接口以支持扩展属性
    /// </summary>
    public class FormField : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 字段键，用于在表单数据中唯一标识该字段
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// 字段显示名称，用于在用户界面中显示该字段的名称
        /// </summary>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// 扩展属性字典，用于存储额外的字段配置信息（如字段类型、验证规则等）
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected FormField()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的表单字段
        /// </summary>
        /// <param name="id">表单字段唯一标识</param>
        /// <param name="key">字段键</param>
        /// <param name="displayName">字段显示名称</param>
        public FormField(
            Guid id,
            string key,
            string displayName)
            : base(id)
        {
            Key = key;
            DisplayName = displayName;
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}



