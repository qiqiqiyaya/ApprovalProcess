using System;
using System.Collections.Generic;
using System.Linq;
using Ap.Core.Converts.ToEntity;
using Ap.Core.Share.Services;
using Ap.Core.StateMachine;
using System.Threading.Tasks;
using Ap.Core.Share.Entities;

namespace Ap.Core.Services
{
    public class StateMachineService(
        EntityConvertContainer entityConvertContainer,
        IExecutableActionService actionService) : IStateMachineService
    {
        public StateMachine<TState, TTrigger> New<TState, TTrigger>(TState initialState)
        {
            return new StateMachine<TState, TTrigger>(initialState);
        }

        public ValueTask SaveAsync<TState, TTrigger>(StateMachine<TState, TTrigger> stateMachine)
        {
            var converter = entityConvertContainer.Get<TState, TTrigger>();

            var sm = new StateMachineEntity();
            sm.InitialState = converter.ToState(stateMachine.InitialState);
            sm.CurrentState = converter.ToState(stateMachine.CurrentState);

            sm.Id = Guid.NewGuid().ToString("N");

            var settingList = new List<StateSettingsEntity>();
            foreach (var setting in stateMachine.StateConfiguration)
            {
                var settingEntity = new StateSettingsEntity()
                {
                    State = converter.ToState(setting.Key),
                    StateMachineId = sm.Id,

                };


                foreach (var behaviour in setting.Value.TriggerBehaviours)
                {

                }


                var aa = await actionService.GetListByNameAsync(setting.Value.EntryActions.ToArray());

                foreach (var behaviour in setting.Value.EntryActions)
                {
                    //actionService.GetListByNameAsync(setting.Value.EntryActions)
                    //settingEntity.Actions = new List<StateSettingsActionEntity>();
                }

            }



        }
    }
}
