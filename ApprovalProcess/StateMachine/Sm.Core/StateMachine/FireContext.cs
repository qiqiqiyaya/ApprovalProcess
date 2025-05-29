using Sm.Core.Actions.Models;
using System;

namespace Sm.Core.StateMachine
{
    public class FireContext<TState, TTrigger>(IServiceProvider serviceProvider, TTrigger trigger)
    {
        public IServiceProvider ServiceProvider { get; } = serviceProvider;

        public TTrigger Trigger { get; } = trigger;
    }

    public class BeforeFireContext<TState, TTrigger> :
        FireContext<TState, TTrigger>
    {
        public BeforeFireContext(IServiceProvider serviceProvider, TTrigger trigger)
            : base(serviceProvider, trigger)
        {

        }

        public BeforeFireContext(FireContext<TState, TTrigger> context)
            : base(context.ServiceProvider, context.Trigger)
        {

        }

        /// <summary>
        /// current state
        /// </summary>
        public StateRepresentation<TState, TTrigger> CurrentRepresentation { get; set; }

        /// <summary>
        /// Next
        /// </summary>
        public StateRepresentation<TState, TTrigger> NextRepresentation { get; set; }

        /// <summary>
        /// destination State
        /// </summary>
        public TState DtState { get; set; }

        public TransitionDescription<TState, TTrigger> TransitionDescription { get; set; }
    }

    public class AfterFireContext<TState, TTrigger> : FireContext<TState, TTrigger>
    {
        public AfterFireContext(IServiceProvider serviceProvider, TTrigger trigger)
            : base(serviceProvider, trigger)
        {

        }

        public AfterFireContext(FireContext<TState, TTrigger> context)
            : base(context.ServiceProvider, context.Trigger)
        {

        }

        /// <summary>
        /// Previous
        /// </summary>
        public StateRepresentation<TState, TTrigger> PreviousRepresentation { get; set; }

        /// <summary>
        /// current state
        /// </summary>
        public StateRepresentation<TState, TTrigger> CurrentRepresentation { get; set; }

        public TransitionDescription<TState, TTrigger> TransitionDescription { get; set; }

        public BeforeFireContext<TState, TTrigger> BeforeFire { get; set; }
    }
}
