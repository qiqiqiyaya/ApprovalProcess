using Volo.Abp.Domain.Entities;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace ApFlow.Domain.Models
{
    /// <summary>
    /// 发起人权限引用
    /// </summary>
    /// <remarks>
    /// 发起人权限引用实体，用于建立发起人权限与其他实体之间的关联关系。
    /// 通过引用实体，可以实现多对多的关系，如用户与发起人权限的关联。
    /// 这种设计模式使得权限系统更加灵活，可以支持复杂的权限分配场景。
    /// </remarks>
    public class StarterPermissionReference : Entity<Guid>
    {
        /// <summary>
        /// 发起人权限标识
        /// </summary>
        /// <remarks>
        /// 引用发起人权限的唯一标识，建立引用与权限的关联关系。
        /// 通过此标识可以获取关联的发起人权限信息。
        /// </remarks>
        public Guid StarterPermissionId { get; protected set; }

        /// <summary>
        /// 关联的发起人权限实体
        /// </summary>
        /// <remarks>
        /// 导航属性，引用关联的发起人权限实体，用于访问权限的完整信息。
        /// </remarks>
        public StarterPermission StarterPermission { get; protected set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <remarks>
        /// 供Entity Framework Core使用的受保护构造函数，用于从数据库加载实体
        /// </remarks>
        protected StarterPermissionReference()
        {
        }

        /// <summary>
        /// 创建新的发起人权限引用
        /// </summary>
        /// <param name="id">引用唯一标识</param>
        /// <param name="starterPermissionId">发起人权限标识</param>
        /// <remarks>
        /// 初始化一个新的发起人权限引用，建立与发起人权限的关联。
        /// 发起人权限标识不能为空，否则会抛出异常。
        /// 创建后可以通过其他属性扩展引用的功能。
        /// </remarks>
        public StarterPermissionReference(Guid id, Guid starterPermissionId)
            : base(id)
        {
            if (starterPermissionId == Guid.Empty)
                throw new ArgumentException("发起人权限ID不能为空", nameof(starterPermissionId));

            StarterPermissionId = starterPermissionId;
        }
    }
}



