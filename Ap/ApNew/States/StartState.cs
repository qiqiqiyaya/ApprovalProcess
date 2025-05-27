using ApNew.Nodes;

namespace ApNew.States
{
    public class StartState : StateBase
    {
        private const string StartStateName = "Start_";

        public StartState(string builderId) : base(StartStateName + builderId)
        {

        }
    }
}
