using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public class StateSetContainer(string name, StateSetBase parent) : StateSetContainerBase(name, parent)
    {
        public override ValueTask InitialEntry(TriggerContext context)
        {
            throw new System.NotImplementedException();
        }

        public override ValueTask CompletedExit(TriggerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
