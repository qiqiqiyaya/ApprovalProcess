using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sm.Core.Actions.Entry;
using Sm.Core.Actions.Models;

namespace Sm.Core.StateMachine
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
        public StateRepresentation<TState, TTrigger> Configure(TState state)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            return result;
        }

        public void ParallelOr(TState state, Action<RepresentationChildrenBuilder<TState, TTrigger>> builderAction)
        {
            if (!StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                result = new StateRepresentation<TState, TTrigger>(state);
                StateConfiguration.Add(state, result);
            }

            var builder = new RepresentationChildrenBuilder<TState, TTrigger>(ParallelRelationship.Or);
            builderAction(builder);

            result.AddChildren(builder.Build());
        }

        /// <summary>
        /// 触发
        /// </summary>
        public async ValueTask Fire(FireContext<TState, TTrigger> context)
        {
            //BeforeFireContext<TState, TTrigger> before = new BeforeFireContext<TState, TTrigger>(context);
            var currentRepresentation = GetRepresentation(CurrentState);
            //before.CurrentRepresentation = currentRepresentation;

            var stateTransition = currentRepresentation.FindTriggerBehaviour(context.Trigger);

            var transition = new Transition<TState, TTrigger>(CurrentState, stateTransition.Destination, context.Trigger);
            await HandleTransitioningTrigger(currentRepresentation, transition);


            //before.NextRepresentation = GetRepresentation(behaviour.Destination);
            //before.DtState = behaviour.Destination;

            //TransitionDescription<TState, TTrigger> td = new TransitionDescription<TState, TTrigger>(CurrentState,
            //    before.DtState,
            //    context.Trigger);
            //before.TransitionDescription = td;

            //Exit(before);
            //CurrentState = before.DtState;

            //AfterFireContext<TState, TTrigger> after = new AfterFireContext<TState, TTrigger>(context);
            //after.CurrentRepresentation = GetRepresentation(CurrentState);
            //after.PreviousRepresentation = currentRepresentation;
            //after.BeforeFire = before;
            //after.TransitionDescription = td;

            //await Entry(after);
        }

        public async ValueTask Fire(TTrigger trigger)
        {
            var currentRepresentation = GetRepresentation(CurrentState);
            var behaviour = currentRepresentation.FindTriggerBehaviour(trigger);


            switch (behaviour)
            {
                case ReentryTriggerBehaviour<TState, TTrigger> reentry:
                    {
                        // Handle transition, and set new state
                        var transition = new Transition<TState, TTrigger>(CurrentState, behaviour.Destination, trigger);
                        HandleReentryTrigger(currentRepresentation, transition);
                        break;
                    }
                case TransitionTriggerBehaviour<TState, TTrigger> transitionTriggerBehaviour:
                    {
                        //await HandleTransitioningTrigger(currentRepresentation, transition);
                        break;
                    }
            }

        }

        private void HandleReentryTrigger(StateRepresentation<TState, TTrigger> representativeState, Transition<TState, TTrigger> transition)
        {

        }

        private async ValueTask HandleTransitioningTrigger(StateRepresentation<TState, TTrigger> representativeState, Transition<TState, TTrigger> transition)
        {
            representativeState.Exit(transition);
            CurrentState = transition.Destination;
            var newRepresentation = GetRepresentation(transition.Destination);
            //await Entry()
        }


        private void Exit(BeforeFireContext<TState, TTrigger> context)
        {
            var settings = context.CurrentRepresentation;
            //settings.Exit();
        }

        private async ValueTask Entry(AfterFireContext<TState, TTrigger> context)
        {
            var settings = context.CurrentRepresentation;
            await settings.Entry(new EntryActionContext<TState, TTrigger>(Id, context.ServiceProvider, context));
        }

        private StateRepresentation<TState, TTrigger> GetRepresentation(TState state)
        {
            if (StateConfiguration.TryGetValue(state, out StateRepresentation<TState, TTrigger> result))
            {
                return result;
            }

            throw new Exception($"状态机没有配置状态 {state}");
        }

        internal StateMachine(string id)
        {
            Id = id;
        }

        internal StateMachine<TState, TTrigger> SetInitialState(TState state)
        {
            InitialState = state;
            return this;
        }

        internal StateMachine<TState, TTrigger> SetCurrentState(TState currentState)
        {
            CurrentState = currentState;
            return this;
        }

        internal StateMachine<TState, TTrigger> SetStateConfigurations(Dictionary<TState, StateRepresentation<TState, TTrigger>> configurations)
        {
            StateConfiguration = configurations;
            return this;
        }
    }
}
