using System.Collections.Generic;

namespace Ap.Share.Models
{
    /// <summary>
    /// 触发记录
    /// </summary>
    public class TriggeredRecord
    {
        public string StateMachineId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState { get; set; }

        public TriggeredType TriggeredType { get; set; }

        /// <summary>
        /// 能操作此状态机的人
        /// </summary>
        public List<Employee> Firer { get; set; }

        /// <summary>
        /// 能操作此状态机的组织
        /// </summary>
        public List<Organization> Organizations { get; set; }
    }

    public enum TriggeredType
    {
        /// <summary>
        /// 创建
        /// </summary>
        Create,
        /// <summary>
        /// 转换
        /// </summary>
        Transition,

    }
}
