namespace ApNew.Nodes.Core
{
    public class TriggerDictionary : Dictionary<string, TriggerResult>
    {
        public TriggerDictionary()
        {

        }

        public TriggerDictionary(IList<TriggerResult> list)
        {
            foreach (var item in list)
            {
                this[item.Trigger] = item;
            }
        }

        public TriggerDictionary(IEnumerable<KeyValuePair<string, TriggerResult>> collection) : base(collection)
        {

        }
    }
}
