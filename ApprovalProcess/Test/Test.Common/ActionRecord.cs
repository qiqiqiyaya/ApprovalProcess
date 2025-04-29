using Ap.Core.Share.Entities;

namespace Test.Common
{
    public class ActionRecord
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ExecutableActionType Type { get; set; }

        public ActionRecord(string name, string description, ExecutableActionType type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
