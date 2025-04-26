using System.Threading.Tasks;
using Ap.Core.StateMachine;

namespace Ap.Core
{
    public interface IStateMachineLoader
    {
        ValueTask<StateMachine<string, string>> GetStateMachine(string id);

        ValueTask<StateSettings<string, string>> GetStateSettings(string id);
    }
}
