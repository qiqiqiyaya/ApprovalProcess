using System.Collections.Generic;
using Ap.Core.Behaviours;

namespace Ap.Core.Definitions
{
    public interface IState : INode
    {
        string Name { get; }

        /// <summary>
        /// Transition to
        /// </summary>
        Dictionary<string, IBehaviour> Transitions { get; }

        void AddTransition(IBehaviour behaviour);

        void Entry();

        void Exit();

        TriggerDictionary GetTrigger();
    }
}
