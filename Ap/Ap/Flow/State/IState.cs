using Ap.Flow.Behaviours;

namespace Ap.Flow.State
{
    public interface IState
    {
        string State { get; }

        IDictionary<string, ICollection<ITriggerBehaviour>> Transitions { get; }

        ITriggerBehaviour FindTriggerBehaviour(string trigger);

        void AddTransition(ITriggerBehaviour triggerBehaviour);

        void Exit(Transition transition);

        void Entry();

        StateBase SkipTo(string state);
    }
}
