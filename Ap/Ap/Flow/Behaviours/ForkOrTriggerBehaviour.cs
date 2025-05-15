using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ap.Flow.Behaviours
{
    public class ForkOrTriggerBehaviour<TState, TTrigger>(TTrigger trigger, TState destination)
        : TriggerBehaviour<TState, TTrigger>(trigger, destination)
    {

    }
}
