using System;
using System.Threading.Tasks;
using Ap.Core.StateMachine;

namespace Ap.Core
{
    public class StateMachineActuator(
        IStateMachineLoader machineLoader,
        IServiceProvider serviceProvider)
        : IStateMachineActuator
    {
        public async ValueTask<StateMachine<string, string>> Fire(string id, string trigger)
        {
            var stateMachine = await machineLoader.GetStateMachine(id);
            await stateMachine.Fire(new FireContext<string, string>(serviceProvider, trigger));
            return stateMachine;
        }
    }
}
