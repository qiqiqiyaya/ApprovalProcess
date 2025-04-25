using System.Threading.Tasks;

namespace ApprovalProcess.Core
{
	public interface IStateMachineActuator
	{
		ValueTask<StateMachine<string, string>> Fire(string id, string trigger);
	}
}
