using System.Threading.Tasks;
using Sm.Core.StateMachine;
using Sm.Share.Entities;

namespace Sm.Core.Services
{
    public interface IStateMachineService
    {
        ValueTask<StateMachineEntity> SaveAsync<TState, TTrigger>(StateMachine<TState, TTrigger> stateMachine);
    }
}
