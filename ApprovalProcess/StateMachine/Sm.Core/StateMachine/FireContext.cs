using System;

namespace Sm.Core.StateMachine
{
    public class FireContext<TState, TTrigger>(IServiceProvider serviceProvider, TTrigger trigger)
    {
        public IServiceProvider ServiceProvider { get; } = serviceProvider;

        public TTrigger Trigger { get; } = trigger;

        public StateSettings<TState, TTrigger> CurrentSettings { get; set; }

        public StateSettings<TState, TTrigger> NextSettings { get; set; }

        public TState DtState { get; set; }
    }
}
