using Ap.Share.Actions.Entry;
using Ap.Share.Actions.Entry.NextApprover;
using System.Threading.Tasks;

namespace Ap.Core.Actions.Entry.NextApprover
{
    public class ApprovedByOrgService : IApprovedByOrgService
    {
        public ApprovedByOrgService()
        {
            // Constructor logic here
        }

        public async ValueTask InvokeAsync(ApEntryActionContext<string, string> context)
        {
            // 下一级审批人为 Firer 上级
            var employee = await context.GetFirerInfo();

            //employee.Organization

        }
    }
}
