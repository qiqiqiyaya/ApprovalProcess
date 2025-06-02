using System.Collections.Generic;

namespace Ap.Core.Definitions
{
    public class TriggerDictionary : Dictionary<string, TriggerResult>
    {
        public TriggerDictionary()
        {

        }

        public TriggerDictionary(IEnumerable<TriggerResult> list)
        {
            foreach (var item in list)
            {
                this[item.Trigger] = item;
            }
        }

        public TriggerDictionary(IEnumerable<KeyValuePair<string, TriggerResult>> collection)
        {
            foreach (var item in collection)
            {
                Add(item.Key, item.Value);
            }
        }
    }
}
