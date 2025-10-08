using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace OAApproval.Models
{
    public class ApprovalTemplate
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; } = true;
        public string FormSchema { get; set; } // JSON字符串，存储表单配置
        public string ProcessDefinition { get; set; } // JSON字符串，存储流程定义
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}