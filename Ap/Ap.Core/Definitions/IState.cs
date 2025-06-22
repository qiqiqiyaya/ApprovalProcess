using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ap.Core.Behaviours;
using Ap.Core.Configurations;

namespace Ap.Core.Definitions
{
    public interface IState : INode
    {
        string Name { get; }

        /// <summary>
        /// Transition to
        /// </summary>
        Dictionary<string, IBehaviour> Transitions { get; }

        StateConfiguration StateConfiguration { get; }

        internal IServiceProvider ServiceProvider { get; set; }

        void AddTransition(IBehaviour behaviour);

        ValueTask Entry(EntryContext context);

        ValueTask Exit(ExitContext context);

        ValueTask<StateTriggerCollection> GetTrigger();

        StateDetail ToDetail();
    }
}
