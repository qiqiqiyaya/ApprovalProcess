using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Core;

namespace ApNew.Nodes.States
{
    public class StartState : StateBase
    {
        private const string StartStateName = "Start_";

        public StartState(string builderId) : base(StartStateName + builderId)
        {

        }

        public INodeBehaviour FindNext()
        {
            var direct = NodeTransitions.Values.FirstOrDefault(x => x.GetType() == typeof(Direct));
            if (direct == null)
            {
                throw new InvalidOperationException($"No direct transition found in {State} state.");
            }

            return direct;
        }
    }
}
