using Ap.Flow.Behaviours;

namespace Ap.Flow.State
{
    /// <summary>
    /// and can't use SkipTo
    /// </summary>
    /// <param name="stateMachine"></param>
    public class BranchAnd(StateMachine stateMachine)
    {
        public Dictionary<string, StateContainer> Containers { get; set; } = new Dictionary<string, StateContainer>();

        public StateContainer Then(string state)
        {
            StateContainer container = new StateContainer(stateMachine);
            container.Id = Guid.NewGuid().ToString("N");

            var node = stateMachine.Linked.Last;
            var previous = node!.Value;

            // 上一个状态开启分支
            previous.AddTransition(new AndBegin(BehaviourConst.AndBegin + "_" + state, state));

            container.Then(state);
            Containers.Add(container.Id, container);
            return container;
        }

        public StateMachine Join(string state)
        {
            foreach (var container in Containers)
            {
                if (container.Value.Linked.Last == null) continue;

                var stateBase = container.Value.Linked.Last.Value;
                stateBase.AddTransition(new AndEnd(BehaviourConst.AndEnd + "_" + state, state));
            }

            foreach (var container in Containers)
            {
                foreach (var innerState in container.Value.StateConfiguration)
                {
                    var stateBase = innerState.Value;
                    stateMachine.StateConfiguration.Add(stateBase.State, stateBase);
                }
            }

            var result = new StateBase(state, stateMachine);

            stateMachine.StateHandleWithEmpty(state, destination =>
            {
                result.AddTransition(new Approve(destination));
                result.AddTransition(new ReturnToStart(destination));
            });
            stateMachine.StateConfiguration.Add(state, result);
            stateMachine.Linked.AddLast(result);
            return stateMachine;
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
