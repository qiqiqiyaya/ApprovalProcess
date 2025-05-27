using ApNew.Nodes;

namespace ApNew.States
{
    public class EndState : StateBase
    {
        private const string EndStateName = "End_";

        public EndState(string builderId) : base(EndStateName + builderId)
        {

        }
    }
}
