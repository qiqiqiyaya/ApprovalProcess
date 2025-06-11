namespace Ap.Core.Behaviours
{
    public class Transition(string trigger, string destination)
    {
        public string Trigger { get; } = trigger;

        public string Destination { get; } = destination;
    }
}
