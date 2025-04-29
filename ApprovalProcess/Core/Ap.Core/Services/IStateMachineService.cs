using Ap.Core.Share.Entities;
using Ap.Core.StateMachine;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
	public interface IStateMachineService
	{
		ValueTask<StateMachineEntity> SaveAsync<TState, TTrigger>(StateMachine<TState, TTrigger> stateMachine);
	}
}
