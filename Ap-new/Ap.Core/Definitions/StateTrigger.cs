// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Ap.Core.Definitions
{
    public class StateTrigger(string trigger, StateDetail stateDetail)
    {
        public string? StateSetId { get; set; }

        public string Trigger { get; set; } = trigger;

        public string StateId => StateDetail.Id;

        public string StateName => StateDetail.Name;

        public StateDetail StateDetail { get; set; } = stateDetail;
    }

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
