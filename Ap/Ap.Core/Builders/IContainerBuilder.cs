using Ap.Core.Definitions;
using System.Collections.Generic;

namespace Ap.Core.Builders
{
    public interface IContainerBuilder
    {
        string Id { get; }

        Dictionary<string, object> StateSetBuilderDic { get; }

        string State { get; }

        StateLinkedList RootStateLinked { get; }

        IContainerStateSetBuilder New(string state, string id);

        IContainerStateSetBuilder New(string state);

        internal IStateSetContainer Build(StateSetBase parent);
    }
}
