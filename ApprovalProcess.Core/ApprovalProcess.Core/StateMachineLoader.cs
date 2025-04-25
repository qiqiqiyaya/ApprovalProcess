using ApprovalProcess.Core.Converts.ToStateMachines;
using ApprovalProcess.Core.Converts.ToStateSettings;
using ApprovalProcess.Core.Entities;
using ApprovalProcess.Core.Repositories;
using System.Threading.Tasks;

namespace ApprovalProcess.Core
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
