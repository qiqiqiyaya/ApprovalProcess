using System.Collections.Generic;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateSettings
{
    public class StateConfiguration<TState, TTrigger>
    {
        public TState State { get; set; }

        public List<Transition<TState, TTrigger>> Transitions { get; set; } = new List<Transition<TState, TTrigger>>();

        public List<string> EntryActionNames { get; set; } = new List<string>();

        public List<string> ExitActionNames { get; set; } = new List<string>();
    }
}
