using System.Reflection;
using Sm.Core.Actions;
using Sm.Core.Actions.Entry;
using Sm.Core.Actions.Models;
using Sm.Share.Actions;

namespace Sm.Register
{
    public class ApRegisterOption
    {
        internal Dictionary<string, ExecutableActionMap> EntryActions { get; set; } = new Dictionary<string, ExecutableActionMap>();

        internal Dictionary<string, ExecutableActionMap> ExitActions { get; set; } = new Dictionary<string, ExecutableActionMap>();

        public void AddEntryAction<TEntryAction, TState, TTrigger>()
            where TEntryAction : IEntryAction<TState, TTrigger>
        {
            var map = AddAction<TEntryAction, TState, TTrigger>();
            if (EntryActions.ContainsKey(map.Action.Name))
            {
                throw new ArgumentException($"Action {map.Action} already registered");
            }

            EntryActions.Add(map.Action.Name, map);
        }

        public void AddExitAction<TEntryAction, TState, TTrigger>()
            where TEntryAction : IEntryAction<TState, TTrigger>
        {
            var map = AddAction<TEntryAction, TState, TTrigger>();
            if (ExitActions.ContainsKey(map.Action.Name))
            {
                throw new ArgumentException($"Action {map.Action} already registered");
            }

            ExitActions.Add(map.Action.Name, map);
        }

        private ExecutableActionMap AddAction<TEntryAction, TState, TTrigger>()
            where TEntryAction : IEntryAction<TState, TTrigger>
        {
            var type = typeof(TEntryAction);
            var actionName = type.GetCustomAttribute<ActionNameAttribute>();
            if (actionName == null)
            {
                throw new ArgumentException($"Action {type.Name} must have ActionNameAttribute");
            }

            var map = new ExecutableActionMap(new StateSettingAction(actionName.Name), type);
            return map;
        }
    }
}
