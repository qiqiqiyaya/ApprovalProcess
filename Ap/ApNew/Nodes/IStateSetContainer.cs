namespace ApNew.Nodes
{
    /// <summary>
    /// Container
    /// </summary>
    public interface IStateSetContainer : IState
    {
        IDictionary<string, IStateSet> StateSets { get; }
    }
}
