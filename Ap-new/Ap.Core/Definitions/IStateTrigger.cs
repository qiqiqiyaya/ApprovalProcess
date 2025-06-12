using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
    public interface IStateTrigger
    {
        ValueTask ExecuteTrigger(TriggerContext context);
    }
}
