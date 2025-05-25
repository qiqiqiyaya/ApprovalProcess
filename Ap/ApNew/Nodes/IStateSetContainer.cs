namespace ApNew.Nodes
{
    /// <summary>
    /// Container
    /// </summary>
    public interface IStateSetContainer : IState, IStateTrigger
    {
        IDictionary<string, IStateSet> StateSets { get; }
    }
}
