using Ap.Flow.Behaviours;
using Ap.Flow.State;

namespace Ap.Flow
{
    public class StateMachine : IStateContainer
    {
        public string Id { get; set; }

        public string CurrentState { get; private set; }

        public IDictionary<string, IState> StateConfiguration { get; set; } =
            new Dictionary<string, IState>();

        internal LinkedList<IState> Linked = new LinkedList<IState>();

        public List<string> GetTriggers()
        {
            var state = GetRepresentation(CurrentState);
            return state.Transitions.Select(s => s.Key).ToList();
        }

        public virtual StateMachine Start(string state)
        {
            if (!StateConfiguration.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state, this);
                StateHandle(state, destination => result.AddTransition(new Submit(destination)));
                StateConfiguration.Add(state, result);
            }

            CurrentState = state;
            Linked.AddFirst(result);
            return this;
        }

        public virtual StateMachine Then(string state)
        {
            if (!StateConfiguration.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state, this);
                StateHandle(state, destination =>
                {
                    result.AddTransition(new Approve(destination));
                    result.AddTransition(new ReturnToStart(destination));
                });
                StateConfiguration.Add(state, result);
            }

            Linked.AddLast(result);
            return this;
        }

        public virtual StateMachine Then(string state, Action<IState> configure)
        {
            if (!StateConfiguration.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state, this);
                StateHandle(state, destination =>
                {
                    result.AddTransition(new Approve(destination));
                    result.AddTransition(new ReturnToStart(destination));
                });
                configure.Invoke(result);
                StateConfiguration.Add(state, result);
            }

            Linked.AddLast(result);
            return this;
        }

        public virtual StateMachine Complete(string state)
        {
            if (!StateConfiguration.TryGetValue(state, out IState? result))
            {
                result = new StateBase(state, this);
                StateHandle(state);
                StateConfiguration.Add(state, result);
            }

            Linked.AddLast(result);
            return this;
        }

        public virtual BranchAnd BranchAnd(Action<BranchAnd> branchAction)
        {
            var and = new BranchAnd(this);
            branchAction.Invoke(and);
            return and;
        }

        public void Trigger(string trigger)
        {
            var currentRepresentation = GetRepresentation(CurrentState);
            HandleBehaviour(currentRepresentation, trigger);
        }

        private void HandleBehaviour(IState representation, string trigger)
        {
            var behaviour = representation.FindTriggerBehaviour(trigger);
            var transition = new Transition(CurrentState, behaviour.Destination, trigger);

            var next = GetRepresentation(behaviour.Destination);
            next.Entry();

            behaviour.InvokeAsync(new BehaviourContext(transition));
            CurrentState = behaviour.Destination;
            representation.Exit(transition);
        }

        private IState GetRepresentation(string state)
        {
            if (StateConfiguration.TryGetValue(state, out IState? result))
            {
                return result;
            }

            throw new Exception($"状态机没有配置状态 {state}");
        }

        private readonly StateHandleCache _cache = new StateHandleCache();
        public virtual void StateHandle(string state, Action<string>? destinationAction = null)
        {
            _cache.Last = _cache.Current;
            _cache.Last?.Invoke(state);

            if (destinationAction != null) _cache.Current = destinationAction;
        }

        public virtual void StateHandleWithEmpty(string state, Action<string>? destinationAction = null)
        {
            _cache.Last = null;
            _cache.Current = null;

            if (destinationAction != null) _cache.Current = destinationAction;
        }
    }

    public class StateHandleCache
    {
        public Action<string>? Last { get; set; }

        public Action<string>? Current { get; set; }
    }
}
