using System.Threading.Tasks;
using Ap.Core.Share.Entities;

namespace Ap.Core.Share.Repositories
{
    public interface IApRepository
    {
        ValueTask<StateMachineEntity> GetStateMachine(string id);

        ValueTask<StateSettingsEntity> GetStateSettings(string id);
    }
}
