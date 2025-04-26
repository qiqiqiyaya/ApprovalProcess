namespace Ap.Core.Share.Entities
{
    public class ExecutableActionEntity : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ExecutableActionType Type { get; set; }
    }

    public enum ExecutableActionType
    {
        Entry = 1,
        Exit = 2
    }
}
