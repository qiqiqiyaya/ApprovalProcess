using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.Converts.ToStateRepresentations
{
    public class StateConfiguration<TState, TTrigger>
    {
        public TState State { get; set; }

        public List<Transition<TState, TTrigger>> Transitions { get; set; } = new List<Transition<TState, TTrigger>>();


    }
}
