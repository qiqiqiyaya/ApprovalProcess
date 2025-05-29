using System.Collections.Generic;
using System.Threading.Tasks;
using Sm.Share.Entities;

namespace Sm.Share.Repositories
{
    public interface ISmRepository
    {
        ValueTask<StateMachineEntity> GetStateMachine(string id);

        ValueTask<StateSettingsEntity> GetStateSettings(string id);

        ValueTask<List<ExecutableActionEntity>> GetExecutableActionAllAsync();

        ValueTask<ExecutableActionEntity> AddActionAsync(ExecutableActionEntity entity);
        ValueTask<StateMachineEntity> SaveAsync(StateMachineEntity entity);

    }
}
