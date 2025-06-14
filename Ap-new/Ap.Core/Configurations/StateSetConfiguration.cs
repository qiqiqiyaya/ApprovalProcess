using Ap.Core.Services.Interfaces;
using System;

namespace Ap.Core.Configurations
{
    public class StateSetConfiguration
    {
        /// <summary>
        /// 获取下级审批人服务
        /// <see cref="IAssignApproverService"/>
        /// </summary>
        public Type? AssignApproverServiceType { get; internal set; }
    }
}
