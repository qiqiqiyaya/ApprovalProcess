using Ap.Share.Actions.Entry;
using Ap.Share.Actions.Entry.NextApprover;
using System.Threading.Tasks;
using Sm.Core.Actions.Entry;

namespace Ap.Core.Actions.Entry.NextApprover
{
    public class ApprovedByOrgService : IApprovedByOrgService
    {
        public ApprovedByOrgService()
        {
            // Constructor logic here
        }

        public async ValueTask InvokeAsync(EntryActionContext<string, string> context)
        {
            // 下一级审批人为 Firer 上级
            var employee = await context.GetEmployeeCacheAsync(context.Firer, EntryActionContextExtensions.FirerInfoKey);

            var aa = employee.Organization.Managers;
        }
    }
}
