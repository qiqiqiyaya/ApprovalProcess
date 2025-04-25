namespace Ap.Core.Actions.Entry
{
    /// <summary>
    /// 当进入 state 时执行 Action
    /// </summary>
    public interface IEntryAction<TState, TTrigger> : IExecutableAction<EntryActionContext<TState, TTrigger>>
    {

    }
}
