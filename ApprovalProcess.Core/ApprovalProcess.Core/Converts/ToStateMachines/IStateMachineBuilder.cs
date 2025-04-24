namespace ApprovalProcess.Core.Converts.ToStateMachines
{
    public interface IStateMachineBuilder<TState, TTrigger>
    {
        IStateMachineBuilder<TState, TTrigger> SetInitialState(TState state);

        IStateMachineBuilder<TState, TTrigger> SetCurrentState(TState state);

        IStateMachineBuilder<TState, TTrigger> SetStateConfiguration(StateSettings<TState, TTrigger> settings);

        StateMachine<TState, TTrigger> Build();
    }
}
