using Ap.Flow.Behaviours;

namespace Ap.Flow
{
    public class StateMachine<TState, TTrigger>
    {
        public StateMachine(TState initialState)
        {
            InitialState = initialState;
            CurrentState = initialState;

        }

        public string Id { get; set; }

        /// <summary>
        /// 初始状态
        /// </summary>
        public TState InitialState { get; private set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public TState CurrentState { get; private set; }

        /// <summary>
        /// 状态表达集
        /// </summary>
        internal IDictionary<TState, StateRepresentation<TState, TTrigger>> StateConfiguration { get; set; } =
            new Dictionary<TState, StateRepresentation<TState, TTrigger>>();

        /// <summary>
        /// 配置一个状态内容
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual StateRepresentation<TState, TTrigger> Configure(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger>? result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }

        public void ParallelOr(TState state, Action<SubStateMachine<TState, TTrigger>> builderAction)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger>? result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            SubStateMachine<TState, TTrigger> children = new SubStateMachine<TState, TTrigger>(state, this);
            builderAction(children);
            result.AddChildren(children);
        }

        public void ParallelAnd(TState state, Action<SubStateMachine<TState, TTrigger>> builderAction)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger>? result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            SubStateMachine<TState, TTrigger> children = new SubStateMachine<TState, TTrigger>(state, this);
            builderAction(children);
            result.AddChildren(children);
        }

        public void Fire(TTrigger trigger)
        {
            var currentRepresentation = GetRepresentation(CurrentState);
            if (currentRepresentation.HasChildren())
            {
                ChildrenFire(currentRepresentation, trigger);
                return;
            }

            HandleBehaviour(currentRepresentation, trigger);
        }

        private void HandleBehaviour(StateRepresentation<TState, TTrigger> representation, TTrigger trigger)
        {
            var behaviour = representation.FindTriggerBehaviour(trigger);
            switch (behaviour)
            {
                case ReentryTriggerBehaviour<TState, TTrigger> reentry:
                    {
                        // Handle transition, and set new state
                        var transition = new Transition<TState, TTrigger>(CurrentState, behaviour.Destination, trigger);
                        HandleReentryTrigger(representation, transition);
                        break;
                    }
                case TransitionTriggerBehaviour<TState, TTrigger> transitionTrigger:
                    {
                        var transition = new Transition<TState, TTrigger>(CurrentState, behaviour.Destination, trigger);
                        HandleTransitioningTrigger(representation, transition);
                        break;
                    }
                case JumpOutTriggerBehaviour<TState, TTrigger> jumpOut:
                    {
                        var transition = new Transition<TState, TTrigger>(CurrentState, behaviour.Destination, trigger);
                        HandleJumpOutTrigger(representation, transition);
                        break;
                    }
            }
        }

        private void ChildrenFire(StateRepresentation<TState, TTrigger> representation, TTrigger trigger)
        {
            representation.Children?.Fire(trigger);
        }

        private void HandleJumpOutTrigger(StateRepresentation<TState, TTrigger> representativeState, Transition<TState, TTrigger> transition)
        {
            representativeState.Exit(transition);
            //var newRepresentation = GetRepresentation(transition.Destination);


        }

        private void HandleReentryTrigger(StateRepresentation<TState, TTrigger> representativeState, Transition<TState, TTrigger> transition)
        {
            representativeState.Exit(transition);
            var newRepresentation = GetRepresentation(transition.Destination);
            CurrentState = newRepresentation.State;
            //representation = await EnterStateAsync(newRepresentation, transition, args);
        }

        private void HandleTransitioningTrigger(StateRepresentation<TState, TTrigger> representativeState, Transition<TState, TTrigger> transition)
        {
            representativeState.Exit(transition);
            CurrentState = transition.Destination;
            var newRepresentation = GetRepresentation(transition.Destination);
            //await Entry()
        }

        private StateRepresentation<TState, TTrigger> GetRepresentation(TState state)
        {
            if (StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger>? result))
            {
                return result;
            }

            throw new Exception($"状态机没有配置状态 {state}");
        }

    }
}
