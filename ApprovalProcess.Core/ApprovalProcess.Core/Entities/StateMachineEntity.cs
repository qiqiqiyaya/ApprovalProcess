using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.Entities
{
    public class StateMachineEntity
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
        public IDictionary<string, StateRepresentationEntity> StateConfiguration { get; set; } =
            new Dictionary<string, StateRepresentationEntity>();
        public StateMachineEntity(string initialState)
        {
            InitialState = initialState;
            State = initialState;
        }
    }
}
