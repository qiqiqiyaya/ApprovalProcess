using System.Collections.Concurrent;

namespace MultiParallelApproval_2Stateless
{
    public class ApprovalProcessContext
    {
        public State CurrentState { get; set; }
        public ApprovalNode CurrentNode { get; set; }
        public ConcurrentBag<ApprovalTask> PendingTasks { get; set; }

        // 原子操作计数器
        private int _completedCount;
        public bool IsParallelCompleted =>
            Interlocked.CompareExchange(ref _completedCount, 0, 0) >= RequiredApprovals;

        public void MarkTaskComplete()
        {
            Interlocked.Increment(ref _completedCount);
        }
    }

    public class ApprovalTask
    {
        public Guid TaskId { get; } = Guid.NewGuid();
        public string Assignee { get; }
        public ApprovalStatus Status { get; private set; }

        public bool IsCompleted => Status != ApprovalStatus.Pending;
        public bool IsApproved => Status == ApprovalStatus.Approved;
        public bool IsRejected => Status == ApprovalStatus.Rejected;

        public ApprovalTask(string assignee) => Assignee = assignee;

        public void Process(ApprovalDecision decision)
        {
            // 线程安全操作
            lock (this)
            {
                if (IsCompleted) return;
                Status = decision.Status;
            }
        }
    }
}
