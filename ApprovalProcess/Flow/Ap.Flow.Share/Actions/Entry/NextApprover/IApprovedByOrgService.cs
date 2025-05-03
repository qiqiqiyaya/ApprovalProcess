using Ap.Core.Actions.Entry;
using System.Threading.Tasks;

namespace Ap.Flow.Share.Actions.Entry.NextApprover
{
    public interface IApprovedByOrgService
    {
        ValueTask InvokeAsync(EntryActionContext<string, string> context);
    }
}
