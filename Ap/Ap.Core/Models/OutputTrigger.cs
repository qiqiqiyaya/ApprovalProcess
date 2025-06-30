using System;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
	public class OutputTrigger
	{
		public string Id { get; set; }

		public string Trigger { get; set; }

		public DateTime ExecuteTime { get; set; }
	}
}
