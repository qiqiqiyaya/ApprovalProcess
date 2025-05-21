using Ap.Flow.Behaviours;
using Ap.Flow.StateRepresentations;

namespace Ap.Flow
{
	public class StateMachine
	{
		public string Id { get; set; }

		public string CurrentState { get; private set; }

		private Action<string>? _lastStateAction;

		internal IDictionary<string, StateRepresentation> StateConfiguration { get; set; } =
			new Dictionary<string, StateRepresentation>();

		internal LinkedList<StateRepresentation> Linked = new LinkedList<StateRepresentation>();

		public virtual StateMachine Start(string initialState)
		{
			if (!StateConfiguration.TryGetValue(initialState, out StateRepresentation? result))
			{
				result = new StateRepresentation(initialState, this);

				_lastStateAction = destination =>
				{
					result.AddTransition(new Submit(destination));
				};
				StateConfiguration.Add(initialState, result);
			}

			CurrentState = initialState;
			Linked.AddFirst(result);
			return this;
		}

		protected virtual void Handle(string state)
		{
			var node = Linked.Last;
			if (node == null) return;


		}

		public virtual StateMachine Then(string state)
		{
			if (!StateConfiguration.TryGetValue(state, out StateRepresentation? result))
			{
				result = new StateRepresentation(state, this);
				_lastStateAction?.Invoke(state);

				_lastStateAction = destination =>
				{
					result.AddTransition(new Approve(destination));
					result.AddTransition(new ReturnToStart(destination));
				};
				StateConfiguration.Add(state, result);
			}

			Linked.AddLast(result);
			return this;
		}

		public virtual StateMachine Complete(string state)
		{
			if (!StateConfiguration.TryGetValue(state, out StateRepresentation? result))
			{
				result = new StateRepresentation(state, this);
				_lastStateAction?.Invoke(state);
				StateConfiguration.Add(state, result);
			}

			Linked.AddLast(result);
			return this;
		}

		public void Trigger(string trigger)
		{
			var currentRepresentation = GetRepresentation(CurrentState);
			HandleBehaviour(currentRepresentation, trigger);
		}

		private void HandleBehaviour(StateRepresentation representation, string trigger)
		{
			var behaviour = representation.FindTriggerBehaviour(trigger);
			var transition = new Transition(CurrentState, behaviour.Destination, trigger);

			var next = GetRepresentation(behaviour.Destination);
			next.Entry();

			behaviour.InvokeAsync(new BehaviourContext(transition));
			CurrentState = behaviour.Destination;
			representation.Exit(transition);
		}

		private StateRepresentation GetRepresentation(string state)
		{
			if (StateConfiguration.TryGetValue(state, out StateRepresentation? result))
			{
				return result;
			}

			throw new Exception($"状态机没有配置状态 {state}");
		}

	}
}
