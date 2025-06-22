using Ap.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ap.Core.Actions;
using Ap.Core.Definitions.Actions;
using Ap.Core.Configurations;

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

    //public override async ValueTask Entry(EntryContext context)
    //{
    //    await context.ActionRunAsync(StateConfiguration);
    //}

    //public override ValueTask Exit(ExitContext context)
    //{
    //    return new ValueTask();
    //}
}
