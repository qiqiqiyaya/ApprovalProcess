using System.Collections.Generic;
using System.Linq;

namespace ApprovalProcess.Core
{
    public class StateMachine<TState, TTrigger>
    {

        public StateMachine(TState initialState)
        {
            InitialState = initialState;
            State = initialState;
        }

        /// <summary>
        /// 初始状态
        /// </summary>
        public TState InitialState { get; protected set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public TState State { get; private set; }

        /// <summary>
        /// 状态表达集
        /// </summary>
        public IDictionary<TState, StateRepresentation<TState, TTrigger>> StateConfiguration { get; set; } =
            new Dictionary<TState, StateRepresentation<TState, TTrigger>>();

        /// <summary>
        /// 配置一个状态内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public StateRepresentation<TState, TTrigger> Configure(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }

        /// <summary>
        /// 触发
        /// </summary>
        public void Fire(TTrigger trigger)
        {
            var source = State;
            var representativeState = GetRepresentation(source);

            representativeState.TryFindBehaviour(trigger, out var triggerBehaviours);

            var behaviour = triggerBehaviours.First();
            State = behaviour.DestinationState;
        }

        private StateRepresentation<TState, TTrigger> GetRepresentation(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }
    }
}
