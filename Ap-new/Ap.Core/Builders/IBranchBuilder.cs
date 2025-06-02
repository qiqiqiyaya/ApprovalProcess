using System.Collections.Generic;

namespace Ap.Core.Builders
{
    public interface IBranchBuilder : IStateBuilder
    {
        string Id { get; }

        Dictionary<string, StateSetBuilder> StateSetBuilderDic { get; }
    }
}
