using System.Collections.Generic;

namespace ApprovalProcess.Core.Entities
{
	public class StateSettingsEntity : Entity
	{
		public string State { get; set; }

		public ICollection<TransitionEntity> Transitions { get; set; } = new List<TransitionEntity>();

		public ICollection<ExecutableActionEntity> ExecutableActions { get; set; } = new List<ExecutableActionEntity>();
	}
}
