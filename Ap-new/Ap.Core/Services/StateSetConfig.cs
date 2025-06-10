using Ap.Core.Configurations;
using Ap.Core.Definitions;

namespace Ap.Core.Services
{
    public class StateSetConfig : ApproverConfiguration
    {
        public IStateSet StateSet { get; set; }

        public StateSetConfig(ApproverConfiguration configuration, IStateSet stateSet)
        {
            PerBiulderType = configuration.PerBiulderType;
            ApproverServiceType = configuration.ApproverServiceType;
            StateSet = stateSet;
        }
    }
}
