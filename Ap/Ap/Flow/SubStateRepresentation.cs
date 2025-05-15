using Ap.Flow.Behaviours;

namespace Ap.Flow
{
	public class SubStateRepresentation<TState, TTrigger> : StateRepresentation<TState, TTrigger>
	{
		public SubStateRepresentation(TState state) : base(state)
		{

		}

		public SubStateRepresentation<TState, TTrigger> JumpOut(TTrigger trigger, TState destinationState)
		{
			EnforceNotIdentityTransition(destinationState);
			var jumpOut = new JumpOutTriggerBehaviour<TState, TTrigger>(trigger, destinationState);
			AddTransition(jumpOut);
			return this;
		}

		public override SubStateRepresentation<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
		{
			return (SubStateRepresentation<TState, TTrigger>)base.Permit(trigger, destinationState);
		}

		public virtual SubStateRepresentation<TState, TTrigger> Finish(TTrigger trigger)
		{
			AddTransition(new OrFinishTriggerBehaviour<TState, TTrigger>(trigger));
			return this;
		}
	}
}
