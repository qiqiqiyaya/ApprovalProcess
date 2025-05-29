using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiParallelApproval
{
    public enum ApprovalNodeType
    {
        Start = 0,
        Parallel = 1,
        Sequential = 2,
        Condition = 3,
        End = 4
    }

    public class ApprovalNode
    {
        public Guid Id { get; set; }
        public ApprovalNodeType NodeType { get; set; }
        public string NodeName { get; set; }
        public List<Guid> NextNodes { get; set; } = new();

        // 审批策略配置
        public ParallelStrategy ParallelStrategy { get; set; }
        public ApproverSelection ApproverSelection { get; set; }
    }

    public class ParallelStrategy
    {
        public CompletionCondition Condition { get; set; } // 完成条件：All/Any
        public ConflictResolution Resolution { get; set; } // 冲突解决策略
    }

    public enum CompletionCondition
    {
        AllApproved = 1,
        AnyApproved = 2,
        Majority = 3
    }
}
