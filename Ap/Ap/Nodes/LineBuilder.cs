using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ap.Nodes.Transitions;

namespace Ap.Nodes
{
    public sealed class LineBuilder
    {
        public IDictionary<string, INode> Nodes { get; } = new Dictionary<string, INode>();

        public LinkedList<INode> LinkedList { get; } = new LinkedList<INode>();

        private Action<string> _addTransition = destination => { };

        public LineBuilder(string state)
        {
            Start(state);
        }

        private void Start(string state)
        {
            if (!Nodes.TryGetValue(state, out INode? result))
            {
                result = new NodeBase(state);

                _addTransition = destination =>
                {
                    result.AddTransition(new Approve(TransitionConst.Approve, destination));
                };
                Nodes.Add(state, result);
            }
            LinkedList.AddFirst(result);
        }

        public LineBuilder Then(string state)
        {
            if (!Nodes.TryGetValue(state, out INode? result))
            {
                result = new NodeBase(state);
                Nodes.Add(state, result);
            }
            LinkedList.AddLast(result);
            _addTransition(state);
            _addTransition = destination =>
            {
                result.AddTransition(new Approve(TransitionConst.Approve, destination));
                result.AddTransition(new Reject(TransitionConst.Reject, destination));
            };
            return this;
        }
    }
}
