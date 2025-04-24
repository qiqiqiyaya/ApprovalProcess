using ApprovalProcess.Core.Entities;
using System.Threading.Tasks;

namespace ApprovalProcess.Core.Repositories
{
    public interface IStateMachineRepository
    {
        ValueTask<StateMachineEntity> GetStateMachine(string id);

        ValueTask<StateSettingsEntity> GetStateSettings(string id);
    }
}
