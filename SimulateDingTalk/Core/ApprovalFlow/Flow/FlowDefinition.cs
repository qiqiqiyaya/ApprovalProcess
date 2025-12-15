using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Flow
{
    /// <summary>
    /// 出入边
    /// </summary>
    public class FlowDefinition : Entity<Guid>, IHasExtraProperties
    {
        public FlowDirection Direction { get; set; }

        public ConditionDefinition ConditionDefinition { get; set; }

        public Guid SourceId { get; set; }

        public Guid TargetId { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; }

        /// <summary>
        /// 流程图
        /// </summary>
        public Guid ApprovalFlowGraphId { get; set; }
        /// <summary>
        /// 流程图
        /// </summary>
        public ApprovalFlowGraph ApprovalFlowGraph { get; set; }
    }
}
