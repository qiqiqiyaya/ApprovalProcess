using Sm.Share.Entities.bases;

namespace Sm.Share.Entities
{
    public class ExecutableActionEntity : CreatorEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ExecutableActionType EventType { get; set; }

        /// <summary>
        /// Configuration类型
        /// </summary>
        public string ActionType { get; set; }
    }

    public enum ExecutableActionType
    {
        Entry = 1,
        Exit = 2
    }
}
