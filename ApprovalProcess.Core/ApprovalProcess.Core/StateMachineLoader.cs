using ApprovalProcess.Core.Entities;
using ApprovalProcess.Core.Repositories;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApprovalProcess.Core.Converts.ToStateSettings;

namespace ApprovalProcess.Core
{
    public class StateMachineLoader : IStateMachineLoader
    {
        private readonly IStateMachineRepository _repository;
        private readonly ToStateSettingsContainer _toStateSettingsContainer;
        private IStateMachineLoader _stateMachineLoaderImplementation;

        public StateMachineLoader(IStateMachineRepository repository,
            ToStateSettingsContainer toStateSettingsContainer)
        {
            _repository = repository;
            _toStateSettingsContainer = toStateSettingsContainer;
        }

        public ValueTask<StateMachine<string, string>> GetStateMachine(string id)
        {

            return new ValueTask<StateMachineEntity>(_approvalProcess);
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
