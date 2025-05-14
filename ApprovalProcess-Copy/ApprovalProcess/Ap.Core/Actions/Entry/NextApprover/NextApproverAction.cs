using System;
using System.Threading.Tasks;
using Ap.Share.Actions;
using Ap.Share.Actions.Entry.NextApprover;
using Sm.Core.Actions.Entry;
using Sm.Share.Actions;

namespace Ap.Core.Actions.Entry.NextApprover
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

        public async ValueTask InvokeAsync(EntryActionContext<string, string> context, Func<EntryActionContext<string, string>, ValueTask> next)
        {
            switch (_configuration.Rule)
            {
                case ApprovalRule.ApprovedByOrg:
                    var aa = context.GetRequiredService<IApprovedByOrgService>();
                    await aa.InvokeAsync(context);
                    break;
                case ApprovalRule.CustomApproval:
                    break;
            }

            await next(context);
        }
    }
}
