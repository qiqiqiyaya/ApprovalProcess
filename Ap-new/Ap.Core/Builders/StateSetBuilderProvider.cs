using Ap.Core.Definitions;
using System;

namespace Ap.Core.Builders
{
    public class StateSetBuilderProvider(IServiceProvider serviceProvider, StateLinkedList? rootStateLinked = null)
        : IStateSetBuilderProvider
    {
        public IServiceProvider ServiceProvider { get; } = serviceProvider;

        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state)
        {
            var obj = (IStateSetBuilder)Activator.CreateInstance(typeof(StateSetBuilder),
                ServiceProvider,
                state,
                rootStateLinked!);

            return (IStateSetBuilder<IStateSetBuilder>)obj;
        }

        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state, Action<IState, string> action)
        {
            var obj = Activator.CreateInstance(typeof(StateSetBuilder),
                ServiceProvider,
                state,
                rootStateLinked!,
                action);
            return (IStateSetBuilder<IStateSetBuilder>)obj;
        }

        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state, string id)
        {
            var obj = Activator.CreateInstance(typeof(StateSetBuilder),
                ServiceProvider,
                state,
                id,
                rootStateLinked!);

            return (IStateSetBuilder<IStateSetBuilder>)obj;
        }

        public virtual IStateSetBuilder<IStateSetBuilder> Create(string state, string id, Action<IState, string> action)
        {
            var obj = Activator.CreateInstance(typeof(StateSetBuilder),
                ServiceProvider,
                state,
                id,
                rootStateLinked!,
                action);

            return (IStateSetBuilder<IStateSetBuilder>)obj;
        }

        public virtual IStateSetBuilder<TStateSetBuilder> Create<TStateSetBuilder>(Func<IStateSetBuilder<TStateSetBuilder>> action)
            where TStateSetBuilder : class
        {
            return action();
        }
    }
}
