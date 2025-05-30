﻿using Sm.Share.Entities.bases;
using System.Collections.Generic;

namespace Sm.Share.Entities
{
    public class StateMachineEntity : CreatorEntity
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        public string InitialState { get; set; }

        /// <summary>
        /// 状态表达集
        /// </summary>
        public ICollection<StateSettingsEntity> StateSettings { get; set; } = new List<StateSettingsEntity>();

    }
}
