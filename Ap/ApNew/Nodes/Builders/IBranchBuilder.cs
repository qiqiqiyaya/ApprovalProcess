namespace ApNew.Nodes.Builders
{
    public interface IBranchBuilder : IStateBuilder
    {
        string Id { get; }

        IDictionary<string, StateSetBuilder> StateSetBuilderDic { get; }
    }
}
