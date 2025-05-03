using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ap.Core.Actions.Pipeline;

namespace Ap.Core.Actions.Entry
{
    public class EntryActionContext<TState, TTrigger>(
        IServiceProvider serviceProvider,
        TTrigger trigger,
        TState state,
        TState dtState)
        : ActionContext(serviceProvider)
    {
        public TTrigger Trigger { get; } = trigger;

        public TState State { get; } = state;

        public TState DtState { get; } = dtState;

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 触发者
        /// </summary>
        public string Firer { get; set; }

        public ValueTask<string> GetFirerAsync()
        {
            return "";
        }
    }
}
