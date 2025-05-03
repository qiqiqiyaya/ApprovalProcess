using System;

namespace Ap.Core.Share.Actions
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class ActionNameAttribute : Attribute
	{
		public string Name { get; set; }

		public ActionNameAttribute(string name)
		{
			Name = name;
		}
	}
}
