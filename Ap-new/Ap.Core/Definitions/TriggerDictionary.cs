using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

        public List<StateNodeTrigger> GetTriggers(string name)
        {
            var node = GetState(name);
            if (node == null) return new List<StateNodeTrigger>();
            return node.ToTriggers();
        }

        public StateNodeTrigger? GetTrigger(string name, string trigger)
        {
            var node = GetState(name);
            if (node == null) return null;
            if (node.Triggers.Contains(trigger))
            {
                return new StateNodeTrigger()
                {
                    StateId = node.StateId,
                    Trigger = trigger,
                    StateSetId = node.StateSetId,
                    StateName = node.StateName
                };
            }

            return null;
        }

        public StateNode? GetState(string name)
        {
            return Values.FirstOrDefault(x => x.StateName == name);
        }
    }

    public class StateNode(string stateId, string stateName)
    {
        public string StateId { get; set; } = stateId;

        public string StateName { get; set; } = stateName;

        public string StateSetId { get; set; }

        public List<string> Triggers { get; set; }

        public List<StateNodeTrigger> ToTriggers()
        {
            return Triggers.Select(s => new StateNodeTrigger()
            {
                StateId = this.StateId,
                Trigger = s,
                StateSetId = this.StateSetId,
                StateName = this.StateName
            }).ToList();
        }
    }

    public class StateNodeTrigger : StateTrigger
    {
        public string StateId { get; set; }

        public string StateName { get; set; }
    }
}
