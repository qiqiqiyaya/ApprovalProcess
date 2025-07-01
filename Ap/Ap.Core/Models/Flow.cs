using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Flow : NodeBase
    {
        public string RootStateSetId { get; set; }

        public List<NodeBase> Nodes { get; set; } = new List<NodeBase>();

        public FlowStatus FlowStatus { get; set; } = FlowStatus.Initial;

        public string? ParentFlowId { get; set; }

        public Node GetExecutingNode()
        {
            return (Node)Nodes.Single(x => x is Node { IsTriggered: true });
        }
    }
}
