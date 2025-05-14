using System.Threading.Tasks;
using Sm.Core.Actions.Entry;

namespace Ap.Share.Actions.Entry.NextApprover
{
    /// <summary>
    /// 按组织等级审批
    /// </summary>
    public interface IApprovedByOrgService
    {
        ValueTask InvokeAsync(EntryActionContext<string, string> context);
    }
}
