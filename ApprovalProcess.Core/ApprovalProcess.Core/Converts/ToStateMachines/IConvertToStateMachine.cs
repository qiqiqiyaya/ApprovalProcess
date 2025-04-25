namespace ApprovalProcess.Core.Converts.ToStateMachines
{
	public interface IConvertToStateMachine<in TParameter, TState, TTrigger>
	{
		StateMachine<TState, TTrigger> To(TParameter parameter);
	}
}
