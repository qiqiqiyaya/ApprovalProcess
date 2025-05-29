using ApNew.Nodes.Core;

namespace ApNew.Nodes
{
    public class TriggerParameter
    {
        public string StateSetId { get; set; }

        public string Trigger { get; set; }

        public StateSetBase? RootStateSet { get; set; }
    }
}
