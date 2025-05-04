using System.Threading.Tasks;
using Sm.Core.StateMachine;

namespace Sm.Core
{
    public interface IStateMachineLoader
    {
        ValueTask<StateMachine<string, string>> GetStateMachine(string id);

        ValueTask<StateSettings<string, string>> GetStateSettings(string id);
    }
}
