﻿using Sm.Core.StateMachine;

namespace Sm.Core.Converts.ToTransitions
{
    /// <summary>
    /// 状态转换器
    /// </summary>
    public interface IConvertToTransition<in TParameter, TState, TTrigger>
    {
        TriggerBehaviour<TState, TTrigger> To(TParameter parameter);
    }
}
