using System;
using System.Collections.Generic;

namespace Ap.Core.Definitions
{
    /// <summary>
    /// Container
    /// </summary>
    public interface IStateSetContainer : IState, IStateTrigger
    {
        Dictionary<string, IStateSet> StateSets { get; }

        bool IsEnd { get; }

        bool IsConfigured(string state);

        void Recover(IServiceProvider serviceProvider, List<IState> level);
    }
}
