using ApNew.Nodes.Core;

namespace ApNew.Nodes.Builders
{
    public class StateSetBuilderProvider(StateLinkedList? rootStateLinked = null)
    {
        public StateSetBuilder Create(string state)
        {
            return new StateSetBuilder(state, rootStateLinked);
        }

        public StateSetBuilder Create(string state, Action<IState, string> action)
        {
            return new StateSetBuilder(state, rootStateLinked, action);
        }

        public StateSetBuilder Create(string state, string id)
        {
            return new StateSetBuilder(state, id, rootStateLinked);
        }

        public StateSetBuilder Create(string state, string id, Action<IState, string> action)
        {
            return new StateSetBuilder(state, id, rootStateLinked, action);
        }

        public TStateSetBuilder Create<TStateSetBuilder>(Func<TStateSetBuilder> action)
            where TStateSetBuilder : StateSetBuilder
        {
            return action();
        }
    }
}
