using System.Collections.Generic;
using System.Linq;

namespace ApprovalProcess.Core
{
    public class StateMachine<TState, TTrigger>
    {
        public StateMachine(TState initialState)
        {
            InitialState = initialState;
        }

        /// <summary>
        /// 初始状态
        /// </summary>
        public TState InitialState { get; protected set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public TState CurrentState { get; private set; }

        /// <summary>
        /// 状态表达集
        /// </summary>
        public IDictionary<TState, StateSettings<TState, TTrigger>> StateConfiguration { get; set; } =
            new Dictionary<TState, StateSettings<TState, TTrigger>>();

        /// <summary>
        /// 配置一个状态内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public StateSettings<TState, TTrigger> Configure(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateSettings<TState, TTrigger> result))
            {
                result = new StateSettings<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Fire(TTrigger trigger)
        {
            var source = CurrentState;
            var representativeState = GetRepresentation(source);

            representativeState.TryFindBehaviour(trigger, out var triggerBehaviours);

            var behaviour = triggerBehaviours.First();
            CurrentState = behaviour.DtState;
        }

        private StateSettings<TState, TTrigger> GetRepresentation(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateSettings<TState, TTrigger> result))
            {
                result = new StateSettings<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }

        internal StateMachine() { }

        internal StateMachine<TState, TTrigger> SetInitialState(TState state)
        {
            CurrentState = state;
            return this;
        }

        internal StateMachine<TState, TTrigger> SetCurrentState(TState currentState)
        {
            CurrentState = currentState;
            return this;
        }

        internal StateMachine<TState, TTrigger> SetStateConfigurations(Dictionary<TState, StateSettings<TState, TTrigger>> configurations)
        {
            StateConfiguration = configurations;
            return this;
        }
    }
}
