using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IStateSetService
    {
        ValueTask Add(StateSetConfig config);

        ValueTask<IStateSet> GetByIdAsync(string stateSetId);

        ValueTask<IStateSet> GetByNameAsync(string name);

        ValueTask<StateSetConfig> GetConfig(string stateSetId);
    }
}
