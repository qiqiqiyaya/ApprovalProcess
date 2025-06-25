using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public interface IStateTrigger
    {
        ValueTask InitialEntry(TriggerContext context);

        ValueTask ExecuteTrigger(TriggerContext context);

        ValueTask CompletedExit(TriggerContext context);
    }
}
