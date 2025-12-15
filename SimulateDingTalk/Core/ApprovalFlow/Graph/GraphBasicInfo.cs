using Volo.Abp.Domain.Entities;

namespace Core.ApprovalFlow.Graph
{
    public class GraphBasicInfo : Entity<Guid>
    {
        public int Version { get; set; }

        public bool IsPublish { get; set; }


        public Guid ApprovalFlowGraphId { get; set; }
        /// <summary>
        /// 流程图
        /// </summary>
        public ApprovalFlowGraph ApprovalFlowGraph { get; set; }
    }
}
