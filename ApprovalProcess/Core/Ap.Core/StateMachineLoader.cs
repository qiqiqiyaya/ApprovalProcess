using System.Threading.Tasks;
using Ap.Core.Converts.ToStateMachines;
using Ap.Core.Converts.ToStateSettings;
using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Ap.Core.StateMachine;

namespace Ap.Core
{
    public class StateMachineLoader : IStateMachineLoader
    {
        private readonly IStateMachineRepository _repository;
        private readonly ToStateSettingsContainer _toStateSettingsContainer;
        private readonly ToStateMachineContainer _toStateMachineContainer;

        public StateMachineLoader(IStateMachineRepository repository,
            ToStateSettingsContainer toStateSettingsContainer,
            ToStateMachineContainer toStateMachineContainer)
        {
            _repository = repository;
            _toStateSettingsContainer = toStateSettingsContainer;
            _toStateMachineContainer = toStateMachineContainer;
        }

        public async ValueTask<StateMachine<string, string>> GetStateMachine(string id)
        {
            var entity = await _repository.GetStateMachine(id);
            var converter = _toStateMachineContainer.Get<StateMachineEntity, string, string>();

            var stateMachine = converter.To(entity);
            return stateMachine;
        }

        public async ValueTask<StateSettings<string, string>> GetStateSettings(string id)
        {
            var entity = await _repository.GetStateSettings(id);

            var converter = _toStateSettingsContainer.Get<StateSettingsEntity, string, string>();
            var stateSettings = converter.To(entity);

            return stateSettings;
        }
    }
}
