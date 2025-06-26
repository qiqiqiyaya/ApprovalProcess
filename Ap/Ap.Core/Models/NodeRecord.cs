using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class NodeRecord : Node
    {
        internal NodeRecord()
        {

        }

        public NodeRecord(Node node)
        {
            Id = Guid.NewGuid().ToString("N");

            FlowId = node.Id;
            LastExecTrigger = node.LastExecTrigger;
            StateId = node.StateId;
            StateName = node.StateName;
            RootStateSetId = node.RootStateSetId;
            StateSetId = node.StateSetId;
            ExecutorId = node.ExecutorId;
            UpdateTime = DateTime.UtcNow;
            CreateTime = node.CreateTime;
            FlowStatus = node.FlowStatus;
            NextExecutors = [.. node.NextExecutors];
        }

        public string FlowId { get; set; }

        public DateTime UpdateTime { get; set; }
    }


}
