using System;
using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class FlowRecord : Flow
    {
        public FlowRecord(Flow flow)
        {
            Id = Guid.NewGuid().ToString("N");

            FlowId = flow.Id;
            StateTrigger = flow.StateTrigger;
            StateId = flow.StateId;
            StateName = flow.StateName;
            RootStateSetId = flow.RootStateSetId;
            CurrentStateSetId = flow.CurrentStateSetId;
            ExecutorId = flow.ExecutorId;
            UpdateTime = DateTime.UtcNow;

            Approvers = flow.Approvers.ConvertAll(a => new NextApproverRecord
            {
                Id = a.Id,
                ObjectId = a.ObjectId,
                FlowId = flow.Id,
                CreateTime = a.CreateTime
            });
        }

        public string FlowId { get; set; }

        public DateTime UpdateTime { get; set; }

        public new List<NextApproverRecord> Approvers { get; set; }
    }


}
