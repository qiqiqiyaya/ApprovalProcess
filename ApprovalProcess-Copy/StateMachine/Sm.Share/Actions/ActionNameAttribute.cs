using System;

namespace Sm.Share.Actions
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
