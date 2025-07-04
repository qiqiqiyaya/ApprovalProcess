﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ap.Core.Definitions
{
    public class StateTriggerCollection : Collection<StateTrigger>
    {
        internal StateTriggerCollection()
        {

        }

        internal StateTriggerCollection(IList<StateTrigger> list) : base(list)
        {

        }

        public StateTrigger? Get(string stateName)
        {
            return this.FirstOrDefault(x => x.StateDetail.Name == stateName);
        }
    }
}
