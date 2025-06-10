using System;

namespace Ap.Core.Configurations
{
    public class ApproverConfiguration
    {
        /// <summary>
        /// 审批流模板
        /// </summary>
        public Type PerBiulderType { get; set; }

        /// <summary>
        /// 获取下级审批人服务
        /// </summary>
        public Type ApproverServiceType { get; set; }
    }
}
