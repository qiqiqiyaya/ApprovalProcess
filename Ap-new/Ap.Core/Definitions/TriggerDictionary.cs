using System.Collections.Generic;

namespace Ap.Core.Definitions
{
    public class TriggerDictionary : Dictionary<string, StateNode>
    {
        public TriggerDictionary()
        {

        }

        public TriggerDictionary(StateNode node)
        {
            Add(node.StateId, node);
        }

        //public TriggerDictionary(IEnumerable<KeyValuePair<string, TriggerResult>> collection)
        //{
        //    foreach (var item in collection)
        //    {
        //        Add(item.Key, item.Value);
        //    }
        //}
    }


    public class StateNode(string stateId, string name)
    {
        public string StateId { get; set; } = stateId;

        public string Name { get; set; } = name;

        public string StateSetId { get; set; }

        public List<string> Triggers { get; set; }
    }
}
