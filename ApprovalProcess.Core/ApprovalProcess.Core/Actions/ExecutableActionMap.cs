#nullable enable
using System;

namespace ApprovalProcess.Core.Actions
{
	public class ExecutableActionMap(string name, Type type)
	{
		public string Name { get; set; } = name;

		public Type Type { get; set; } = type;
	}
}
