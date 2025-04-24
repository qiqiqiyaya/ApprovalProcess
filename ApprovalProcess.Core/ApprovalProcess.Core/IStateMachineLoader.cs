using System.Threading.Tasks;

namespace ApprovalProcess.Core
{
    public interface IStateMachineLoader
    {
        ValueTask<StateMachine<string, string>> GetStateMachine(string id);

        ValueTask<StateSettings<string, string>> GetStateSettings(string id);
    }
}
