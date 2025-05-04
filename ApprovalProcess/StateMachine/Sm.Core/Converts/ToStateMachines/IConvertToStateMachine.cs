using System.Threading.Tasks;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateMachines
{
    public interface IConvertToStateMachine<in TParameter, TState, TTrigger>
    {
        ValueTask<StateMachine<TState, TTrigger>> To(TParameter parameter);
    }
}
