using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public interface IStateSetBuilderProvider
    {
        public IServiceProvider ServiceProvider { get; }

        IStateSetBuilder<IStateSetBuilder> Create(string state);

        IStateSetBuilder<IStateSetBuilder> Create(string state, Action<IState, string> action);

        IStateSetBuilder<IStateSetBuilder> Create(string state, string id);

        IStateSetBuilder<IStateSetBuilder> Create(string state, string id, Action<IState, string> action);

        IStateSetBuilder<TStateSetBuilder> Create<TStateSetBuilder>(Func<IStateSetBuilder<TStateSetBuilder>> action)
            where TStateSetBuilder : class;
    }
}
