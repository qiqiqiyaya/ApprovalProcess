using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Core.ApprovalFlow.Node
{
    /// <summary>
    /// 节点定义
    /// </summary>
    public class NodeDefinition : Entity<Guid>, IHasExtraProperties
    {
        public NodeTypeDefinition TypeDefinition { get; set; }

        public ApproverDefinition ApproverDefinition { get; set; }

        public ICollection<NodeAction> Actions { get; set; }

        /// <summary>
        /// 表单权限
        /// </summary>
        public ICollection<NodeFormPermissions> FormPermissions { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        public Guid ApprovalFlowGraphId { get; set; }
        /// <summary>
        /// 流程图
        /// </summary>
        public ApprovalFlowGraph ApprovalFlowGraph { get; set; }
    }
}
