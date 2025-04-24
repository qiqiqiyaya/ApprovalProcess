using ApprovalProcess.Core.Converts.ToTransitions;
using ApprovalProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApprovalProcess.Core.Converts.ToStateSettings
{
    public class EntityToStateSettings : IConvertToStateSettings<StateSettingsEntity, string, string>
    {
        private readonly TransitionConContainer _container;

        public EntityToStateSettings(TransitionConContainer container)
        {
            _container = container;
        }

        public StateSettings<string, string> To(StateSettingsEntity parameter)
        {
            IStateSettingsBuilder<string, string> builder = new StateSettingsBuilder<string, string>();

            var converter = _container.Get<TransitionEntity, string, string>();

            var tsCollection = parameter.Transitions ?? throw new ArgumentNullException("");

            var transitions = tsCollection.Select(s => converter.To(s)).ToList();
            builder.SetTransitions(transitions.ToArray());

            builder.SetState(parameter.State);

            List<string> entryActions = new List<string>();
            List<string> exitActions = new List<string>();
            foreach (var action in parameter.ExecutableActions)
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
