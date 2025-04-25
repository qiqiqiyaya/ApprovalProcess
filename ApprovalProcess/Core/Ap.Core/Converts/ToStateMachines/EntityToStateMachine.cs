using System.Linq;
using Ap.Core.Converts.ToStateSettings;
using Ap.Core.Share.Entities;
using Ap.Core.StateMachine;

namespace Ap.Core.Converts.ToStateMachines
{
    public class EntityToStateMachine : IConvertToStateMachine<StateMachineEntity, string, string>
    {
        private readonly ToStateSettingsContainer _container;

        public EntityToStateMachine(ToStateSettingsContainer container)
        {
            _container = container;
        }

        public StateMachine<string, string> To(StateMachineEntity parameter)
        {
            IStateMachineBuilder<string, string> builder = new StateMachineBuilder<string, string>();

            var converter = _container.Get<StateSettingsEntity, string, string>();
            var settingsList = parameter.StateSettings.Select(s => converter.To(s)).ToList();

            builder.SetInitialState(parameter.InitialState)
                .SetCurrentState(parameter.CurrentState);

            foreach (var item in settingsList)
            {
                builder.SetStateConfiguration(item);
            }

            return builder.Build();
        }
    }
}
