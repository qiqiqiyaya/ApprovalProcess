using Ap.Core.Behaviours;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ap.Core.Definitions.States;

public class StartState(string builderId) : StateBase(StartStateName + builderId)
{
	private const string StartStateName = "Start_";

	public IBehaviour GetBehaviour()
	{
		var direct = Transitions.Values.FirstOrDefault(x => x.GetType() == typeof(Direct));
		if (direct == null)
		{
			throw new InvalidOperationException($"No direct transition found in {Name} state.");
		}

		return direct;
	}

	public override ValueTask Entry(EntryContext context)
	{
		return new ValueTask();
	}
}
