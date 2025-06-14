using Ap.Core.Definitions.Actions;
using Ap.Core.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Ap.Core.Configurations
{
    public class StateConfiguration
    {
        public List<ApAction> EntryTypes { get; } = new();

        public List<ApAction> ExitTypes { get; } = new();

        /// <summary>
        /// 获取下级审批人服务
        /// <see cref="IAssignApproverService"/>
        /// </summary>
        public Type? AssignApproverServiceType { get; internal set; }
    }
}
