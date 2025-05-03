using Ap.Core.Actions;
using Ap.Core.Actions.Entry;
using Ap.Core.Share.Actions;
using System.Reflection;

namespace Ap.Register
{
	public class ApRegisterOption
	{
		internal Dictionary<string, ExecutableActionMap> EntryActions { get; set; } = new Dictionary<string, ExecutableActionMap>();

		internal Dictionary<string, ExecutableActionMap> ExitActions { get; set; } = new Dictionary<string, ExecutableActionMap>();

		public void AddEntryAction<TEntryAction, TState, TTrigger>()
			where TEntryAction : IEntryAction<TState, TTrigger>
		{
			var map = AddAction<TEntryAction, TState, TTrigger>();
			if (EntryActions.ContainsKey(map.Name))
			{
				throw new ArgumentException($"Action {map.Name} already registered");
			}

			EntryActions.Add(map.Name, map);
		}

		public void AddExitAction<TEntryAction, TState, TTrigger>()
			where TEntryAction : IEntryAction<TState, TTrigger>
		{
			var map = AddAction<TEntryAction, TState, TTrigger>();
			if (ExitActions.ContainsKey(map.Name))
			{
				throw new ArgumentException($"Action {map.Name} already registered");
			}

			ExitActions.Add(map.Name, map);
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

			var map = new ExecutableActionMap(actionName.Name, type);
			return map;
		}
	}
}
