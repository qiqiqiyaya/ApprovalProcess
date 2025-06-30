using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Flow : NodeBase
    {
        public string RootStateSetId { get; set; }

        public List<Node> Nodes { get; set; }

        public FlowStatus FlowStatus { get; set; }

        private LinkedList<Node>? _nodeLinked;
        public LinkedList<Node> NodeLinked
        {
            get
            {
                _nodeLinked = _nodeLinked ??= new LinkedList<Node>(Nodes);
                return _nodeLinked;
            }
        }

        public Node GetExecutingNode()
        {
            return NodeLinked.Last.Value;
        }
    }
}
