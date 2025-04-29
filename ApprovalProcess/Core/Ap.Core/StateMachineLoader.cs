using System.Threading.Tasks;
using Ap.Core.Converts.ToStateMachines;
using Ap.Core.Converts.ToStateSettings;
using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Ap.Core.StateMachine;

namespace Ap.Core
{
    public class StateMachineLoader(
        IApRepository repository,
        ToStateSettingsContainer toStateSettingsContainer,
        ToStateMachineContainer toStateMachineContainer)
        : IStateMachineLoader
    {
        public async ValueTask<StateMachine<string, string>> GetStateMachine(string id)
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
