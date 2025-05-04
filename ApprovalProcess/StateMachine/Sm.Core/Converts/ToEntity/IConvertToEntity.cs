namespace Sm.Core.Converts.ToEntity
{
    public interface IConvertToEntity<in TState, in TTrigger>
    {
        string ToState(TState state);

        string ToTrigger(TTrigger trigger);
    }
}
