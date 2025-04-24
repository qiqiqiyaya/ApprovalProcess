namespace ApprovalProcess.Core.Converts.ToStateSettings
{
    public interface IConvertToStateSettings<in TParameter, TState, TTrigger>
    {
        StateSettings<TState, TTrigger> To(TParameter parameter);
    }
}
