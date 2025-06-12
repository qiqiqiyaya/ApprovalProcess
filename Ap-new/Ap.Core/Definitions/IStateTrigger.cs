namespace Ap.Core.Definitions
{
    public interface IStateTrigger
    {
        void ExecuteTrigger(TriggerContext context);
    }
}
