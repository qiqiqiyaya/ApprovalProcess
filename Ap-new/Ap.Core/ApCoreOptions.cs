using Ap.Core.Builders;
using Ap.Core.Configurations;
using Ap.Core.Services.Interfaces;
using System.Collections.Generic;

namespace Ap.Core
{
    public class ApCoreOptions
    {
        public List<ApproverConfiguration> ApproverConfigurations = new List<ApproverConfiguration>();

        public void AddApproverConfig<TPreBuilder, TApproverService>()
            where TPreBuilder : IPreBuilder, new()
            where TApproverService : IApproverService
        {
            ApproverConfigurations.Add(new ApproverConfiguration()
            {
                ApproverServiceType = typeof(TApproverService),
                PerBiulderType = typeof(TPreBuilder)
            });
        }
    }
}
