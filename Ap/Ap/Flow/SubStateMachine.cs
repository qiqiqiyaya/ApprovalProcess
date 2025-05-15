namespace Ap.Flow
{
    public class SubStateMachine<TState, TTrigger>(TState initialState, StateMachine<TState, TTrigger> parent)
        : StateMachine<TState, TTrigger>(initialState)
    {
        private StateMachine<TState, TTrigger> _parent = parent;

        /// <summary>
        /// 配置一个状态内容
        /// </summary>
        /// <returns></returns>
        public override SubStateRepresentation<TState, TTrigger> Configure(TState state)
        {
            //var state = InitialState;
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger>? result))
            {
                result = new SubStateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return (SubStateRepresentation<TState, TTrigger>)result;
        }
    }
}
