﻿using System.Collections.Generic;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
	public class FlowContainer : NodeBase
	{
		public List<Flow> Flows { get; set; }
	}
}
