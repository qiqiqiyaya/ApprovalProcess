using System.Collections.Generic;

namespace ApprovalProcess.Core.Entities
{
    public class StateMachineEntity : Entity
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        public string InitialState { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 状态表达集
        /// </summary>
        public ICollection<StateRepresentationEntity> StateRepresentations { get; set; } = new List<StateRepresentationEntity>();

    }
}
