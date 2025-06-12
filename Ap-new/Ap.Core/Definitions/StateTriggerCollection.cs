using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ap.Core.Definitions
{
    public class StateTriggerCollection : Collection<StateTrigger>
    {
        public StateTriggerCollection()
        {

        }

        public StateTriggerCollection(IList<StateTrigger> list) : base(list)
        {

        }

        public StateTrigger? Get(string stateName)
        {
            return this.FirstOrDefault(x => x.StateName == stateName);
        }
    }
}
