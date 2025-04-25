using System.Threading.Tasks;
using Ap.Core.StateMachine;

namespace Ap.Core
{
    public interface IStateMachineActuator
    {
        ValueTask<StateMachine<string, string>> Fire(string id, string trigger);
    }
}
