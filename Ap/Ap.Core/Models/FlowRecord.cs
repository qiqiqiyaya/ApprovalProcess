﻿using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class FlowRecord : Flow
    {
        internal FlowRecord()
        {

        }

        public FlowRecord(Flow flow)
        {
            Id = Guid.NewGuid().ToString("N");

            FlowId = flow.Id;
            LastExecTrigger = flow.LastExecTrigger;
            StateId = flow.StateId;
            StateName = flow.StateName;
            RootStateSetId = flow.RootStateSetId;
            StateSetId = flow.StateSetId;
            ExecutorId = flow.ExecutorId;
            UpdateTime = DateTime.UtcNow;
            CreateTime = flow.CreateTime;
            FlowStatus = flow.FlowStatus;
            NextExecutors = [.. flow.NextExecutors];
        }

        public string FlowId { get; set; }

        public DateTime UpdateTime { get; set; }
    }


}
