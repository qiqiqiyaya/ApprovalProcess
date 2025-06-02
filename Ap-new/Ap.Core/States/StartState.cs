using System;
using System.Linq;
using Ap.Core.Behaviours;
using Ap.Core.Definitions;

namespace Ap.Core.States
{
    public class StartState(string builderId) : StateBase(StartStateName + builderId)
    {
        private const string StartStateName = "Start_";

        public IBehaviour FindNext()
        {
            var direct = Transitions.Values.FirstOrDefault(x => x.GetType() == typeof(Direct));
            if (direct == null)
            {
                throw new InvalidOperationException($"No direct transition found in {Name} state.");
            }

            return direct;
        }
    }
}
