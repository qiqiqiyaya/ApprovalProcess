using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace OAApproval.Models
{
    public class ApprovalInstance
    {
        public int Id { get; set; }
        public string InstanceNo { get; set; }
        public int TemplateId { get; set; }
        [Required]
        public string Title { get; set; }
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string FormData { get; set; } // JSON字符串，存储表单数据
        public ApprovalStatus Status { get; set; } = ApprovalStatus.Draft;
        public DateTime ApplyTime { get; set; } = DateTime.Now;
        public DateTime? FinishTime { get; set; }
        public virtual ApprovalTemplate Template { get; set; }
        public virtual ICollection<ApprovalTask> Tasks { get; set; }
    }

    public enum ApprovalStatus
    {
        Draft = 0,
        Processing = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4
    }
}