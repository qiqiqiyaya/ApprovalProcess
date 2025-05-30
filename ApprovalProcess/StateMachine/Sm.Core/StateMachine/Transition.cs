﻿namespace Sm.Core.StateMachine
{
    /// <summary>
    /// Describes a state transition.
    /// </summary>
    public class Transition<TState, TTrigger>
    {
        /// <summary>
        /// Construct a transition.
        /// </summary>
        /// <param name="source">The state transitioned from.</param>
        /// <param name="destination">The state transitioned to.</param>
        /// <param name="trigger">The trigger that caused the transition.</param>
        public Transition(TState source, TState destination, TTrigger trigger)
        {
            Source = source;
            Destination = destination;
            Trigger = trigger;
        }

        /// <summary>
        /// The state transitioned from.
        /// </summary>
        public TState Source { get; }

        /// <summary>
        /// The state transitioned to.
        /// </summary>
        public TState Destination { get; }

        /// <summary>
        /// The trigger that caused the transition.
        /// </summary>
        public TTrigger Trigger { get; }
    }
}
