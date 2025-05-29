using System.Threading.Tasks;
using Sm.Core.StateMachine;

namespace Sm.Core
{
    public interface IStateMachineActuator
    {
        ValueTask<StateMachine<string, string>> Fire(string id, string trigger);
    }
}
