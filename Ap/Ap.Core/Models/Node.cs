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
        }

        public string FlowId { get; set; }

        public Flow Flow { get; set; }

        /// <summary>
        /// Whether triggered
        /// </summary>
        public bool IsTriggered { get; internal set; } = false;

        /// <summary>
        /// List of next executors for the flow. These are the users who will be responsible for the next steps in the flow.
        /// </summary>
        public List<NextExecutor> NextExecutors { get; set; } = new();

        public List<NodeAction> EntryActions { get; set; } = new();

        public List<NodeAction> ExitActions { get; set; } = new();

        public void Entry(List<ApAction> actions)
        {
            EntryActions = actions.Select(s => new NodeAction()
            {
                Id = Guid.NewGuid().ToString("N"),
                ActionName = s.Type.Name,
                ExecutingTime = DateTime.UtcNow
            }).ToList();
        }

        public void Exit(OutputTrigger trigger, List<ApAction> actions)
        {
            OutputTrigger = trigger;

            ExitActions = actions.Select(s => new NodeAction()
            {
                Id = Guid.NewGuid().ToString("N"),
                ActionName = s.Type.Name,
                ExecutingTime = DateTime.UtcNow
            }).ToList();
        }
    }
}
