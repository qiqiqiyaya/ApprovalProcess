using Ap.Core.Configurations;
using Ap.Core.Definitions;

namespace Ap.Core.Services
{
    public class StateSetConfig
    {
        /// <summary>
        /// 审批流
        /// </summary>
        public IStateSet StateSet { get; set; }

        /// <summary>
        /// 审批人配置
        /// </summary>
        public ApproverConfiguration ApproverConfig { get; set; }

        public StateSetConfig(ApproverConfiguration config, IStateSet stateSet)
        {
            ApproverConfig = config;
            StateSet = stateSet;
        }
    }
}
