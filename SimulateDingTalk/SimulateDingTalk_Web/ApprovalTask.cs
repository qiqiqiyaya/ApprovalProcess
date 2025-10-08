using System;
using System.ComponentModel.DataAnnotations;

namespace OAApproval.Models
{
    public class ApprovalTask
    {
        public int Id { get; set; }
        public int InstanceId { get; set; }
        public string TaskName { get; set; }
        public string AssigneeId { get; set; }
        public string AssigneeName { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public int Step { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
        public DateTime? ProcessTime { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public virtual ApprovalInstance Instance { get; set; }
    }

    public enum TaskStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Forwarded = 3
    }
}