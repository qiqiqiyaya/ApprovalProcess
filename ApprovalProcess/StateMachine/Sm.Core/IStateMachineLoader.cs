using System.Threading.Tasks;
using Sm.Core.StateMachine;

namespace Sm.Core
{
	public interface IStateMachineLoader
	{
		ValueTask<StateMachine<string, string>> GetStateMachineAsync(string id);

		ValueTask<StateMachine<string, string>> GetStateMachineAsync(string id, string currentState);

		ValueTask<StateRepresentation<string, string>> GetStateSettings(string id);
	}
}
