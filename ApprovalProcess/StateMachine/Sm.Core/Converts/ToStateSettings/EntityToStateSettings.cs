#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Sm.Core.Actions.Models;
using Sm.Core.Converts.ToTransitions;
using Sm.Core.Services;
using Sm.Core.StateMachine;
using Sm.Share.Entities;

namespace Sm.Core.Converts.ToStateSettings
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

            var ids = parameter.Actions.Select(s => s.ExecutableActionId).ToArray();
            var actionDic = await executableActionService.GetListByIdAsync(ids);

            foreach (var action in parameter.Actions)
            {
                var val = actionDic[action.ExecutableActionId];

                ActionConfiguration? configuration = null;
                if (action.Configuration != null)
                {

                    //var aa = Assembly.Load("Ap.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

                    //var aaaa = aa.GetType("Ap.Core.Actions.Entry.CleanNextApprover", true);
                    var type = Type.GetType(action.ConfigurationType, true);
                    if (type == null)
                    {
                        throw new ArgumentNullException($"Type {action.ConfigurationType} not found");
                    }

                    configuration = (ActionConfiguration)JsonSerializer.Deserialize(action.Configuration, type)!;
                }

                if (val.EventType == ExecutableActionType.Entry)
                {
                    builder.SetEntryAction(val.Name, configuration);
                }
                else
                {
                    builder.SetExitAction(val.Name);
                }
            }

            return builder.Build();
        }
    }
}
