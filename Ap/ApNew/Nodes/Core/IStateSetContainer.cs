namespace ApNew.Nodes.Core
{
    /// <summary>
    /// Container
    /// </summary>
    public interface IStateSetContainer : IState, IStateTrigger
    {
        IDictionary<string, IStateSet> StateSets { get; }

        bool IsEnd { get; }

        bool IsConfigured(string state);

    }
}
