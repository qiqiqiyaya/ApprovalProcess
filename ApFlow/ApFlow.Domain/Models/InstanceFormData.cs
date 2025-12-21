using Volo.Abp.Domain.Entities;

namespace ApFlow.Domain.Models;

/// <summary>
/// 实例表单数据
/// </summary>
/// <remarks>
/// 实例表单数据实体，存储审批流程实例中表单的实际数据。
/// 它将表单定义与用户提交的具体数据关联起来，是审批流程中数据传递的核心载体。
/// 每个审批实例可以有多个表单数据，用于不同阶段或不同类型的审批信息收集。
/// </remarks>
public class InstanceFormData : Entity<Guid>
{
    /// <summary>
    /// 关联的表单标识
    /// </summary>
    /// <remarks>
    /// 引用表单定义的唯一标识，建立实例数据与表单模板的关联关系。
    /// 通过此关联可以获取表单的结构定义和字段配置信息。
    /// </remarks>
    public Guid FormId { get; set; }

    /// <summary>
    /// 表单字段数据集合
    /// </summary>
    /// <remarks>
    /// 包含表单中所有字段的具体值，是用户在审批流程中提交的实际数据。
    /// 每个字段值都对应表单定义中的一个字段，存储字段的类型和实际内容。
    /// </remarks>
    public ICollection<FormFieldValue> FieldValues { get; set; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    /// <remarks>
    /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
    /// </remarks>
    protected InstanceFormData()
    {
        FieldValues = new List<FormFieldValue>();
    }

    /// <summary>
    /// 创建新的实例表单数据
    /// </summary>
    /// <param name="id">实例表单数据唯一标识</param>
    /// <param name="formId">关联的表单标识</param>
    /// <remarks>
    /// 初始化一个新的实例表单数据，关联指定的表单定义。
    /// 创建后可以通过添加字段值来填充表单的实际数据内容。
    /// </remarks>
    public InstanceFormData(
        Guid id,
        Guid formId)
        : base(id)
    {
        FormId = formId;
        FieldValues = new List<FormFieldValue>();
    }
}



