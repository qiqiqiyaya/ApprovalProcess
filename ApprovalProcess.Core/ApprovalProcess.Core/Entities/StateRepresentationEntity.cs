using System.Collections.Generic;

namespace ApprovalProcess.Core.Entities
{
    public class StateRepresentationEntity : Entity
    {
        public string State { get; set; }

        public ICollection<TransitionEntity> Transitions { get; set; } = new List<TransitionEntity>();
    }
}
