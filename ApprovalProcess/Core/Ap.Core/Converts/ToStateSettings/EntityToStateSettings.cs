using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ap.Core.Converts.ToTransitions;
using Ap.Core.Services;
using Ap.Core.Share.Entities;
using Ap.Core.StateMachine;

namespace Ap.Core.Converts.ToStateSettings
{
    public class EntityToStateSettings(ToTransitionContainer container,
        IExecutableActionService executableActionService)
        : IConvertToStateSettings<StateSettingsEntity, string, string>
    {
        public async ValueTask<StateSettings<string, string>> To(StateSettingsEntity parameter)
        {
            IStateSettingsBuilder<string, string> builder = new StateSettingsBuilder<string, string>();

            var converter = container.Get<TransitionEntity, string, string>();

            var tsCollection = parameter.Transitions ?? throw new ArgumentNullException("");

            var transitions = tsCollection.Select(s => converter.To(s)).ToList();
            builder.SetTransitions(transitions.ToArray());

            builder.SetState(parameter.State);

            List<string> entryActions = new List<string>();
            List<string> exitActions = new List<string>();

            var ids = parameter.Actions.Select(s => s.ExecutableActionId).ToArray();
            var actions = await executableActionService.GetListByIdAsync(ids);

            foreach (var action in actions)
            {
                if (action.Type == ExecutableActionType.Entry)
                {
                    entryActions.Add(action.Name);
                }
                else
                {
                    exitActions.Add(action.Name);
                }
            }

            builder.SetEntryActions(entryActions.ToArray());
            builder.SetExitActions(exitActions.ToArray());

            return builder.Build();
        }
    }
}
