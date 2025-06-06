using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public interface IIfBuilderProvider
    {
        IStateSetBuilder<IStateSetBuilder> Create(string state);

        IStateSetBuilder<IStateSetBuilder> Create(string state, Action<IState, string> action);
    }
}
