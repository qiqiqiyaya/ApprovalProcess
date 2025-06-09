using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{

    public interface IApproverService
    {
        ValueTask<List<string>> GetListAsync();
    }
}
