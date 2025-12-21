using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 发起人权限
    /// </summary>
    /// <remarks>
    /// 发起人权限实体，定义了审批流程发起人的权限信息。
    /// 发起人权限只能被创建，不能被修改或删除，确保了权限的稳定性和安全性。
    /// 发起人权限控制了哪些用户可以启动特定的审批流程，是审批流程安全控制的重要组成部分。
    /// </remarks>
    public class StarterPermission : Entity<Guid>, ICreationAuditedObject
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        /// <remarks>
        /// 发起人权限的名称，用于标识和描述权限的用途。
        /// 权限名称应该清晰明了，能够反映权限的业务含义。
        /// </remarks>
        public string Name { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <remarks>
        /// 记录权限的创建时间，用于审计和追踪
        /// </remarks>
        public DateTime CreationTime { get; protected set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        /// <remarks>
        /// 记录权限的创建者标识，用于审计和权限控制
        /// </remarks>
        public Guid? CreatorId { get; protected set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected StarterPermission()
        {
        }

        /// <summary>
        /// 创建新的发起人权限
        /// </summary>
        /// <param name="id">权限唯一标识</param>
        /// <param name="name">权限名称</param>
        /// <remarks>
        /// 初始化一个新的发起人权限，设置权限名称。
        /// 权限名称不能为空，否则会抛出异常。
        /// 创建后的权限不能被修改或删除，确保权限的稳定性。
        /// </remarks>
        public StarterPermission(Guid id, string name)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("权限名称不能为空", nameof(name));

            Name = name;
        }
    }
}



