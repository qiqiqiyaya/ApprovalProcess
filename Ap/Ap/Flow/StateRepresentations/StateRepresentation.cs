using Ap.Flow.Behaviours;

namespace Ap.Flow.StateRepresentations
{
	/// <summary>
	/// 状态表示
	/// </summary>
	public class StateRepresentation
	{
		protected StateMachine? _stateMachine;

		public StateRepresentation(string state, StateMachine stateMachine)
		{
			State = state;
			_stateMachine = stateMachine;
		}

		public string State { get; private set; }

		public IDictionary<string, ICollection<ITriggerBehaviour>> Transitions =
			new Dictionary<string, ICollection<ITriggerBehaviour>>();

		internal List<string> ExitActions { get; private set; } = new List<string>();

		public virtual StateRepresentation Permit(string trigger, string destinationState)
		{
			EnforceNotIdentityTransition(destinationState);
			AddTransition(new TransitionTo(trigger, destinationState));
			return this;
		}

		public StateRepresentation PermitReentry(string trigger)
		{
			AddTransition(new Reentry(trigger, State));
			return this;
		}

		internal void AddTransition(ITriggerBehaviour triggerBehaviour)
		{
			if (!Transitions.TryGetValue(triggerBehaviour.Trigger, out ICollection<ITriggerBehaviour>? allowed))
			{
				allowed = new List<ITriggerBehaviour>();
				Transitions.Add(triggerBehaviour.Trigger, allowed);
			}

			allowed.Add(triggerBehaviour);
		}

		public ITriggerBehaviour FindTriggerBehaviour(string trigger)
		{
			ICollection<ITriggerBehaviour> transitions = new List<ITriggerBehaviour>();
			if (Transitions.TryGetValue(trigger, out ICollection<ITriggerBehaviour>? possible))
			{
				transitions = possible;
			}

			if (transitions.Count <= 1) return transitions.FirstOrDefault();

			var message = $"Multiple permitted exit transitions are configured from state '{State}' for trigger '{trigger}'. Guard clauses must be mutually exclusive.";
			throw new InvalidOperationException(message);
		}

		protected void EnforceNotIdentityTransition(string destination)
		{
			if (destination.Equals(State))
			{
				throw new ArgumentException($"Permit() (and PermitIf()) require that the destination state is not equal to the source state. To accept a trigger without changing state, use either Ignore() or PermitReentry().");
			}
		}

		public void Exit(Transition transition)
		{
			ExecuteExitActions(transition);
		}

		public void Entry()
		{

		}

		void ExecuteExitActions(Transition transition)
		{

		}

		public StateRepresentation SkipTo(string state)
		{
			AddTransition(new SkipTo(state));
			return this;
		}
	}

}
