using Ap.Core.Actions;
using Ap.Core.Actions.Entry;
using Ap.Core.Share.Actions;
using Ap.Flow.Share.Actions.Models;
using System;
using System.Threading.Tasks;

namespace Ap.Flow.Share.Actions.Entry.NextApprover
{
    [ActionName(ExecutableActionNames.OnEntrySetNextApprover)]
    public class NextApproverAction : IEntryAction<string, string>
    {
        public string Id { get; set; }

        public string Name { get; }

        private readonly NextApproverConfiguration _configuration;

        public NextApproverAction(NextApproverConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
        {
            if (_configuration.Rule == ApprovalRule.ApprovedByOrg)
            {

            }

            switch (_configuration.Rule)
            {
                case ApprovalRule.ApprovedByOrg:
                    context.GetRequiredService<IApprovedByOrgService>()
                    break;
                case ApprovalRule.CustomApproval:
                    break;
            }

            return next(context);
        }
    }
}
