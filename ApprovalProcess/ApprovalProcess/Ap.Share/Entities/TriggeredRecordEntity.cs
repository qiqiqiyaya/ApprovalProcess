using System.Collections.Generic;

namespace Ap.Share.Entities
{
    public class TriggeredRecordEntity
    {
        public string Id { get; set; }

        public string StateMachineId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState { get; set; }

        public ICollection<NextApproverEntity> NextApproverList { get; set; } = new List<NextApproverEntity>();
    }
}
