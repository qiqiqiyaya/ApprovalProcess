namespace Ap.Core.Share.Entities
{
    public class StateSettingsActionEntity : Entity
    {
        public string StateSettingsId { get; set; }

        public StateSettingsEntity StateSettings { get; set; }

        public string ExecutableActionId { get; set; }


        /// <summary>
        /// Configuration类型
        /// </summary>
        public string ConfigurationType { get; set; }

        /// <summary>
        /// Action配置Json
        /// </summary>
        public string Configuration { get; set; }
    }
}
