using Sm.Core.Converts.ToEntity;
using Sm.Core.StateMachine;
using Sm.Share.Entities;
using Sm.Share.Repositories;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sm.Core.Services
{
    public class StateMachineService(
        EntityConvertContainer entityConvertContainer,
        IExecutableActionService actionService,
        ISmRepository smRepository) : IStateMachineService
    {
        public async ValueTask<StateMachineEntity> SaveAsync<TState, TTrigger>(StateMachine<TState, TTrigger> stateMachine)
        {
            var converter = entityConvertContainer.Get<TState, TTrigger>();

            var sm = new StateMachineEntity
            {
                InitialState = converter.ToState(stateMachine.InitialState),
                //CurrentState = converter.ToState(stateMachine.CurrentState),
                Id = Guid.NewGuid().ToString("N")
            };

            foreach (var setting in stateMachine.StateConfiguration)
            {
                var settingEntity = new StateSettingsEntity()
                {
                    State = converter.ToState(setting.Key),
                    StateMachineId = sm.Id,
                    Id = Guid.NewGuid().ToString("N")
                };

                var trList = setting.Value.TriggerBehaviours.SelectMany(s => s.Value).ToList();
                foreach (var transition in trList)
                {
                    settingEntity.Transitions.Add(new TransitionEntity()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Trigger = converter.ToTrigger(transition.Trigger),
                        DtState = converter.ToState(transition.DtState),
                        StateSettingsId = settingEntity.Id,
                    });
                }

                var names = setting.Value.EntryActions.Select(s => s.Name).ToArray();
                var actionDic = await actionService.GetListByNameAsync(names);

                foreach (var entryAction in setting.Value.EntryActions)
                {
                    var eaEntity = actionDic[entryAction.Name];

                    var settingsActionEntity = new StateSettingsActionEntity()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        StateSettingsId = settingEntity.Id,
                        ExecutableActionId = eaEntity.Id,
                    };

                    if (entryAction.Configuration != null)
                    {
                        settingsActionEntity.Configuration = JsonSerializer.Serialize(entryAction.Configuration, entryAction.Configuration.GetType());
                        settingsActionEntity.ConfigurationType = entryAction.Configuration.GetType().FullName;
                    }

                    settingEntity.Actions.Add(settingsActionEntity);
                }

                sm.StateSettings.Add(settingEntity);
            }

            var entity = await smRepository.SaveAsync(sm);
            return entity;
        }
    }
}
