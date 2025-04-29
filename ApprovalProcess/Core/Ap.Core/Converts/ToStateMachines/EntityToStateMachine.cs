using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async ValueTask<StateMachine<string, string>> To(StateMachineEntity parameter)
        {
            IStateMachineBuilder<string, string> builder = new StateMachineBuilder<string, string>();

            var converter = _container.Get<StateSettingsEntity, string, string>();

            var settingsList = new List<StateSettings<string, string>>();
            foreach (var setting in parameter.StateSettings)
            {
                settingsList.Add(await converter.To(setting));
            }

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
