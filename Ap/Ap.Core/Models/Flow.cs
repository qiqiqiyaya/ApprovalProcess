using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Flow : NodeBase
    {
        public List<NodeBase> Nodes { get; set; } = new();

        public FlowStatus FlowStatus { get; set; } = FlowStatus.Initial;

        public NodeBase GetTriggeredNode()
        {
            return Nodes.FirstOrDefault(x => x.IsTriggered);
        }
    }
}
