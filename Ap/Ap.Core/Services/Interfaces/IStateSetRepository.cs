using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IStateSetRepository
    {
        ValueTask Add(IStateSet set);

        ValueTask<IStateSet> GetByIdAsync(string stateSetId);

        ValueTask<IStateSet> GetByNameAsync(string name);
    }
}
