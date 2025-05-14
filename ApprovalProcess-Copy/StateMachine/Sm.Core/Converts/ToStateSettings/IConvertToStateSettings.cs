using System.Threading.Tasks;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateSettings
{
    public interface IConvertToStateSettings<in TParameter, TState, TTrigger>
    {
        ValueTask<StateSettings<TState, TTrigger>> To(TParameter parameter);
    }
}
