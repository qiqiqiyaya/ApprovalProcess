using Ap.Flow.Behaviours;

namespace Ap.Flow.State
{
    public class StateContainer(StateMachine stateMachine) : IStateContainer
    {
        public string Id { get; set; }

        internal LinkedList<StateBase> Linked = new LinkedList<StateBase>();

        public IDictionary<string, IState> StateConfiguration { get; set; } =
            new Dictionary<string, IState>();

        public StateContainer Then(string state)
        {
            var result = new StateBase(state, stateMachine);
            StateHandle(state, destination =>
            {
                result.AddTransition(new Approve(destination));
                result.AddTransition(new ReturnToStart(destination));
            });
            StateConfiguration.Add(state, result);
            Linked.AddLast(result);
            return this;
        }

        private readonly StateHandleCache _cache = new StateHandleCache();
        protected virtual void StateHandle(string state, Action<string>? destinationAction = null)
        {
            _cache.Last = _cache.Current;
            _cache.Last?.Invoke(state);

            if (destinationAction != null) _cache.Current = destinationAction;
        }
    }
}
