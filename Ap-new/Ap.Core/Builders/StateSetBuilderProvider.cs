using System;
using Ap.Core.Definitions;

namespace Ap.Core.Builders
{
    public class StateSetBuilderProvider(StateLinkedList? rootStateLinked = null)
    {
        public virtual IStateSetBuilder<StateSetBuilder> Create(string state)
        {
            return new StateSetBuilder(state, rootStateLinked);
        }

        public virtual StateSetBuilder Create(string state, Action<IState, string> action)
        {
            return new StateSetBuilder(state, rootStateLinked, action);
        }

        public virtual StateSetBuilder Create(string state, string id)
        {
            return new StateSetBuilder(state, id, rootStateLinked);
        }

        public virtual StateSetBuilder Create(string state, string id, Action<IState, string> action)
        {
            return new StateSetBuilder(state, id, rootStateLinked, action);
        }

        public virtual TStateSetBuilder Create<TStateSetBuilder>(Func<TStateSetBuilder> action)
            where TStateSetBuilder : IStateSetBuilder<TStateSetBuilder>
        {
            return action();
        }
    }
}
