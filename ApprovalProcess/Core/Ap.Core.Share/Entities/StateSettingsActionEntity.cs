namespace Ap.Core.Share.Entities
{
    public class StateSettingsActionEntity : Entity
    {
        public string StateSettingsId { get; set; }

        public StateSettingsEntity StateSettings { get; set; }

        public string ExecutableActionId { get; set; }
    }
}
