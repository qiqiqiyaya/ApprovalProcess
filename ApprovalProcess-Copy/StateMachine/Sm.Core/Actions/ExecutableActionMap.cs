#nullable enable
using Sm.Core.Actions.Models;
using System;

namespace Sm.Core.Actions
{
    public class ExecutableActionMap(StateSettingAction action, Type type)
    {
        public StateSettingAction Action { get; set; } = action;

        public Type Type { get; set; } = type;
    }
}
