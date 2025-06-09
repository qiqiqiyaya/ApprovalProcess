using System;

namespace Ap.Core.Configurations
{
    public class ApproverConfiguration
    {
        public string Name { get; set; }

        /// <summary>
        /// 审批流模板
        /// </summary>
        public string StateSetId { get; set; }

        /// <summary>
        /// 获取下级审批人服务
        /// </summary>
        public Type ServiceType { get; set; }
    }
}
