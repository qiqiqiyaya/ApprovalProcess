using Sm.Share.Entities.bases;
using System.Collections.Generic;

namespace Sm.Share.Entities
{
    public class StateSettingsEntity : CreatorEntity
    {
        public string State { get; set; }

        public ICollection<TransitionEntity> Transitions { get; set; } = new List<TransitionEntity>();

        /// <summary>
        /// Action <see cref="ExecutableActionEntity"/> id
        /// </summary>
        public ICollection<StateSettingsActionEntity> Actions { get; set; } = new List<StateSettingsActionEntity>();

        public string StateMachineId { get; set; }

        public StateMachineEntity StateMachine { get; set; }

    }
}
