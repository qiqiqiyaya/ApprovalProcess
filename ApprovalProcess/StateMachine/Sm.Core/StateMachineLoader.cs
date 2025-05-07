using System.Threading.Tasks;
using Sm.Core.Converts.ToStateMachines;
using Sm.Core.Converts.ToStateSettings;
using Sm.Core.StateMachine;
using Sm.Share.Entities;
using Sm.Share.Repositories;

namespace Sm.Core
{
    public class StateMachineLoader(
        ISmRepository repository,
        ToStateSettingsContainer toStateSettingsContainer,
        ToStateMachineContainer toStateMachineContainer)
        : IStateMachineLoader
    {
        public async ValueTask<StateMachine<string, string>> GetStateMachineAsync(string id)
        {
            var entity = await repository.GetStateMachine(id);
            var converter = toStateMachineContainer.Get<StateMachineEntity, string, string>();


            var stateMachine = await converter.To(entity);
            return stateMachine;
        }

        public async ValueTask<StateSettings<string, string>> GetStateSettings(string id)
        {
            var entity = await repository.GetStateSettings(id);

            var converter = toStateSettingsContainer.Get<StateSettingsEntity, string, string>();
            var stateSettings = await converter.To(entity);

            return stateSettings;
        }
    }
}
