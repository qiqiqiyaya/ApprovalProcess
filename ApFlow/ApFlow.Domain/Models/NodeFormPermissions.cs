using ApFlow.Domain.Models.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 审批节点表单权限
    /// </summary>
    /// <remarks>
    /// 审批节点表单权限实体，定义了在特定审批节点上对表单字段的访问权限。
    /// 通过表单权限配置，可以控制审批人在不同审批阶段对表单字段的访问权限，
    /// 如只读、可编辑、隐藏等。这种细粒度的权限控制确保了审批流程中数据的安全性和合规性。
    /// </remarks>
    public class NodeFormPermissions : Entity<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 审批节点表单字段权限定义
        /// </summary>
        /// <remarks>
        /// 定义表单字段的具体权限类型，如只读、可编辑、隐藏等。
        /// 字段权限决定了审批人在该节点上对表单字段的具体操作权限。
        /// </remarks>
        public FormFieldPermissions FieldPermissions { get; set; }

        /// <summary>
        /// 关联的表单字段标识
        /// </summary>
        /// <remarks>
        /// 引用表单字段的唯一标识，建立权限与字段的关联关系。
        /// 通过此关联可以获取权限所控制的表单字段信息。
        /// </remarks>
        public Guid FormFieldId { get; set; }

        /// <summary>
        /// 关联的表单字段实体
        /// </summary>
        /// <remarks>
        /// 导航属性，引用关联的表单字段实体，用于访问字段的完整信息。
        /// </remarks>
        public FormField FormField { get; set; }

        /// <summary>
        /// 扩展属性字典
        /// </summary>
        /// <remarks>
        /// 用于存储表单权限的扩展属性，如权限条件、自定义配置等。
        /// 扩展属性提供了灵活的方式来存储权限特有的配置信息。
        /// </remarks>
        public ExtraPropertyDictionary ExtraProperties { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected NodeFormPermissions()
        {
            ExtraProperties = new ExtraPropertyDictionary();
        }

        /// <summary>
        /// 创建新的审批节点表单权限
        /// </summary>
        /// <param name="id">权限唯一标识</param>
        /// <param name="fieldPermissions">字段权限类型</param>
        /// <param name="formFieldId">关联的表单字段标识</param>
        /// <remarks>
        /// 初始化一个新的审批节点表单权限，设置字段权限类型和关联的字段。
        /// 创建后可以通过扩展属性添加额外的权限配置信息。
        /// </remarks>
        public NodeFormPermissions(
            Guid id,
            FormFieldPermissions fieldPermissions,
            Guid formFieldId)
            : base(id)
        {
            FieldPermissions = fieldPermissions;
            FormFieldId = formFieldId;
            ExtraProperties = new ExtraPropertyDictionary();
        }
    }
}



