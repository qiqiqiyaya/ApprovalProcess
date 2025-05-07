namespace Sm.Core.Actions.Models
{
    public class TransitionDescription<TState, TTrigger>
    {
        public TransitionDescription(TState source, TState destination, TTrigger trigger)
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
