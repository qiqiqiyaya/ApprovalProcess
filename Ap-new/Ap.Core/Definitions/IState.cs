using System.Collections.Generic;
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

        void Entry();

        void Exit();

        TriggerDictionary GetTrigger();
    }
}
