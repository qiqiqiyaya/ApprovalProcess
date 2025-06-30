#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Ap.Core.Definitions.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ap.Core.Models
{
    public class Node : NodeBase
    {
        public Node(Flow flow)
        {
            Flow = flow;
            FlowId = flow.Id;

            StateSetId = flow.StateSetId;
        }

        public string FlowId { get; set; }

        public Flow Flow { get; set; }

        public NodeStatus Status { get; set; }

        public DateTime UpdateTime { get; set; }

        public void Entry(List<ApAction> actions)
        {
            Status = NodeStatus.Entry;
            UpdateTime = DateTime.UtcNow;

            ExitActions = actions.Select(s => new NodeAction()
            {
                Id = Guid.NewGuid().ToString("N"),
                ActionName = s.Type.Name
            }).ToList();
        }

        public void Exit(OutputTrigger trigger, List<ApAction> actions)
        {
            Status = NodeStatus.Exit;
            UpdateTime = DateTime.UtcNow;
            OutputTrigger = trigger;

            ExitActions = actions.Select(s => new NodeAction()
            {
                Id = Guid.NewGuid().ToString("N"),
                ActionName = s.Type.Name
            }).ToList();
        }
    }

    public enum NodeStatus
    {
        Entry = 1,
        Exit = 2
    }
}
