using System.Collections.Generic;

namespace Ap.Core.Share.Entities
{
    public class StateSettingsEntity : Entity
    {
        public string State { get; set; }

        public ICollection<TransitionEntity> Transitions { get; set; } = new List<TransitionEntity>();

        /// <summary>
        /// Action <see cref="ExecutableActionEntity"/> id
        /// </summary>
        public string ActionListJson { get; set; }

        public string StateMachineId { get; set; }

        public StateMachineEntity StateMachine { get; set; }

    }
}
