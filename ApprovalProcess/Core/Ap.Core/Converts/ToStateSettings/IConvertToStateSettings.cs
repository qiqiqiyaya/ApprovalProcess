using Ap.Core.StateMachine;
using System.Threading.Tasks;

namespace Ap.Core.Converts.ToStateSettings
{
    public interface IConvertToStateSettings<in TParameter, TState, TTrigger>
    {
        ValueTask<StateSettings<TState, TTrigger>> To(TParameter parameter);
    }
}
