using System;
using Sm.Core.Actions.Models;
using System.Collections.Generic;

namespace Ap.Share.Actions.Entry.NextApprover
{
    /// <summary>
    /// 设置下级审批人配置
    /// </summary>
    [Serializable]
    public class NextApproverConfiguration : ActionConfiguration
    {
        public ApprovalRule Rule { get; set; }

        /// <summary>
        /// 自定义设置
        /// </summary>
        public List<CustomInput> Inputs { get; set; }
    }

    public class CustomInput
    {
        public InputRule InputRule { get; set; }

        public string Code { get; set; }
    }

    public enum InputRule
    {
        Org = 1,
        Emp,
    }

    /// <summary>
    /// 审批规则
    /// </summary>
    public enum ApprovalRule
    {
        /// <summary>
        /// 按组织审批
        /// </summary>
        ApprovedByOrg,
        /// <summary>
        /// 自定义审批人
        /// </summary>
        CustomApproval
    }
}
