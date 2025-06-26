using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap.Core.Definitions
{
	/// <summary>
	/// State set
	/// </summary>
	public interface IStateSet : IState, IStateTrigger
	{
		/// <summary>
		/// Initial state
		/// </summary>
		string InitialState { get; }

		string CurrentState { get; }

		bool IsInitial { get; }

		IState CurrentStateNode { get; }

		/// <summary>
		/// Dictionary
		/// </summary>
		Dictionary<string, IState> StateDictionary { get; }

		StateLinkedList LinkedList { get; }

		StateLinkedList RootLinkedList { get; }

		bool IsEnd { get; }

		void AddState(IState state);

		void Recover(IServiceProvider serviceProvider, string stateName);

		void Recover(IServiceProvider serviceProvider, string stateName, List<IState> level);

		/// <summary>
		/// reset ot initial state
		/// </summary>
		void Reset();

		ValueTask InitialEntry(TriggerContext context);

		ValueTask CompletedExit(TriggerContext context);
	}
}
