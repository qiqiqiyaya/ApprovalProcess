using System.Collections.Generic;
using System.Threading.Tasks;
using Ap.Core.Behaviours;
using Ap.Core.Definitions.Actions;

namespace Ap.Core.Definitions
{
    public interface IState : INode
    {
        string Name { get; }

        /// <summary>
        /// Transition to
        /// </summary>
        Dictionary<string, IBehaviour> Transitions { get; }

        ActionConfiguration ActionConfiguration { get; }

        void AddTransition(IBehaviour behaviour);

        ValueTask Entry(EntryContext context);

        void Exit();

        StateTriggerCollection GetTrigger();

        StateDetail ToDetail();
    }
}
