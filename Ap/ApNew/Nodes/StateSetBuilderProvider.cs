namespace ApNew.Nodes
{
    public class StateSetBuilderProvider
    {
        /// <summary>
        /// with start node
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public StateSetBuilder Create(string state)
        {
            return new StateSetBuilder(state);
        }

        public StateSetBuilder Create(string state, string id)
        {
            return new StateSetBuilder(state, id);
        }

        public StateSetBuilder Create(string state, string id, Action<IState, string> action)
        {
            return new StateSetBuilder(state, id, action);
        }
    }
}
