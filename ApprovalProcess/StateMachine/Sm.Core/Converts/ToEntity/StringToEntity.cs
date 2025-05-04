namespace Sm.Core.Converts.ToEntity
{
    public class StringToEntity : IConvertToEntity<string, string>
    {
        public string ToState(string state)
        {
            return state;
        }

        public string ToTrigger(string trigger)
        {
            return trigger;
        }
    }
}
