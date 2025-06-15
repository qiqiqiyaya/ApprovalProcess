using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IStateSetService
    {
        ValueTask Add(IStateSet configuration);

        ValueTask<IStateSet> GetByIdAsync(string stateSetId);

        ValueTask<IStateSet> GetByNameAsync(string name);
    }
}
