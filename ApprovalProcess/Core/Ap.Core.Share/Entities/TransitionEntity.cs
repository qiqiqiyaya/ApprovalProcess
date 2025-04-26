namespace Ap.Core.Share.Entities
{
    public class TransitionEntity : Entity
    {
        /// <summary>
        /// 触发器
        /// </summary>
        public string Trigger { get; set; }

        /// <summary>
        /// DestinationState 目标状态
        /// </summary>
        public string DtState { get; set; }

        public string StateSettingsId { get; set; }

        public StateSettingsEntity StateSettings { get; set; }
    }
}
