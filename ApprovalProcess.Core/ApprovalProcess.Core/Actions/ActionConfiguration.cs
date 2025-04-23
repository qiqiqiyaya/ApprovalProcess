using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.Actions
{
	public class ActionConfiguration
	{
		public string Key { get; set; }

		public Type Type { get; set; }

		public object SmAction { get; set; }
	}
}
