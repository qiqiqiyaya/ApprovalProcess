#nullable enable
using System.Collections.Generic;
using Sm.Core.Actions.Models;
using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToStateSettings
{
    public class StateConfiguration<TState, TTrigger>
    {
        public TState State { get; set; }

        public List<Transition<TState, TTrigger>> Transitions { get; set; } = new List<Transition<TState, TTrigger>>();

        public Dictionary<string, ActionConfiguration?> EntryActionNames { get; set; } = new Dictionary<string, ActionConfiguration?>();

        public Dictionary<string, ActionConfiguration?> ExitActionNames { get; set; } = new Dictionary<string, ActionConfiguration?>();
    }
}
