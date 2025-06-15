using System.Threading.Tasks;
using Ap.Core.Definitions;

namespace Ap.Core.Behaviours
{
    public abstract class BehaviourBase(string trigger, string destination) : IBehaviour
    {
        public string Trigger { get; } = trigger;

        public string Destination { get; } = destination;

        public virtual ValueTask ExecuteAsync(TriggerContext context)
        {
            context.CurrentSet.CurrentState = Destination;
            return new ValueTask();
        }

        public Transition ToMap()
        {
            return new Transition(Trigger, Destination);
        }
    }
}
