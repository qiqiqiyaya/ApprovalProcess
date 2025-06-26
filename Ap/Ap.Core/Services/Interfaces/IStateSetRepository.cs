using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IStateSetRepository
    {
        ValueTask AddAsync(IStateSet set);

        ValueTask<IStateSet> GetByIdAsync(string stateSetId);

        ValueTask<IStateSet> GetByNameAsync(string name);
    }
}
