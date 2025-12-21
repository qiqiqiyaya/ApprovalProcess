using Volo.Abp.Domain.Entities;

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 表单字段值实体，存储表单字段的具体数据值
    /// 继承自ABP框架的Entity，用于记录表单实例中各字段的实际值
    /// </summary>
    public class FormFieldValue : Entity<Guid>
    {
        /// <summary>
        /// 字段值类型，标识该字段值的数据类型（如字符串、数字、日期等）
        /// </summary>
        public string Type { get; protected set; }

        /// <summary>
        /// 字段值，存储该字段的实际数据内容
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// 关联的表单字段标识，指向对应的表单字段定义
        /// </summary>
        public string FormFieldId { get; protected set; }

        /// <summary>
        /// 关联的表单字段对象，表示对应的表单字段定义
        /// </summary>
        public FormField FormField { get; protected set; }

        /// <summary>
        /// 受保护的无参构造函数，供EF Core使用
        /// </summary>
        protected FormFieldValue()
        {
        }

        /// <summary>
        /// 创建新的表单字段值
        /// </summary>
        /// <param name="id">表单字段值唯一标识</param>
        /// <param name="type">字段值类型</param>
        /// <param name="value">字段值</param>
        /// <param name="formFieldId">关联的表单字段标识</param>
        public FormFieldValue(
            Guid id,
            string type,
            string value,
            string formFieldId)
            : base(id)
        {
            Type = type;
            Value = value;
            FormFieldId = formFieldId;
        }
    }
}



