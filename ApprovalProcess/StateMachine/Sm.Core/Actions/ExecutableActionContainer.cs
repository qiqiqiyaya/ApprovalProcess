using System;
using System.Collections.Generic;
using Sm.Core.Actions.Models;

namespace Sm.Core.Actions
{
    /// <summary>
    /// 所有已配置的可执行action容器
    /// </summary>
    public class ExecutableActionContainer(
        Dictionary<string, ExecutableActionMap> entryActionContainer,
        Dictionary<string, ExecutableActionMap> exitActionContainer)
    {
        public List<ExecutableActionMap> GetEntryActions(params StateSettingAction[] settingActions)
        {
            List<ExecutableActionMap> actions = new List<ExecutableActionMap>();
            foreach (var action in settingActions)
            {
                var map = entryActionContainer[action.Name];
                if (map == null)
                {
                    throw new ArgumentNullException($"Entry action {action.Name} not found.");
                }

                actions.Add(map);
            }

            return actions;
        }

        public List<ExecutableActionMap> GetExitActions(params string[] names)
        {
            List<ExecutableActionMap> actions = new List<ExecutableActionMap>();
            foreach (var name in names)
            {
                var map = exitActionContainer[name];
                if (map == null)
                {
                    throw new ArgumentNullException($"Entry action {name} not found.");
                }

                actions.Add(map);
            }

            return actions;
        }
    }
}
