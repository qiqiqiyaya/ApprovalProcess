using Ap.Core.StateMachine;
using System.Threading.Tasks;

namespace Ap.Core.Converts.ToStateMachines
{
    public interface IConvertToStateMachine<in TParameter, TState, TTrigger>
    {
        ValueTask<StateMachine<TState, TTrigger>> To(TParameter parameter);
    }
}
