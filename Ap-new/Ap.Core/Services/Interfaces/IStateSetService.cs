using System.Threading.Tasks;

namespace Ap.Core.Services.Interfaces
{
    public interface IStateSetService
    {
        ValueTask Add(StateSetConfig config);

        ValueTask<StateSetConfig> GetByIdAsync(string stateSetId);

        ValueTask<StateSetConfig> GetByNameAsync(string name);
    }
}
