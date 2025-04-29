using Ap.Core.Converts.ToEntity;
using Ap.Core.Share.Entities;
using Ap.Core.Share.Repositories;
using Ap.Core.StateMachine;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class StateMachineService(
        EntityConvertContainer entityConvertContainer,
        IExecutableActionService actionService,
        IApRepository apRepository) : IStateMachineService
    {
        public async ValueTask<StateMachineEntity> SaveAsync<TState, TTrigger>(StateMachine<TState, TTrigger> stateMachine)
        {
            var converter = entityConvertContainer.Get<TState, TTrigger>();

            var sm = new StateMachineEntity
            {
                InitialState = converter.ToState(stateMachine.InitialState),
                CurrentState = converter.ToState(stateMachine.CurrentState),
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

                var actionEntities = await actionService.GetListByNameAsync(setting.Value.EntryActions.ToArray());
                foreach (var executableAction in actionEntities)
                {
                    settingEntity.Actions.Add(new StateSettingsActionEntity()
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        StateSettingsId = settingEntity.Id,
                        ExecutableActionId = executableAction.Id,
                    });
                }

                sm.StateSettings.Add(settingEntity);
            }

            var entity = await apRepository.SaveAsync(sm);
            return entity;
        }
    }
}
