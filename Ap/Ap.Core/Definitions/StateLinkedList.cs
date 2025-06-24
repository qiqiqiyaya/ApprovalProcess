using Ap.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Ap.Core.Definitions
{
    public class StateLinkedList : LinkedList<IState>
    {
        internal StateLinkedList() { }

        internal StateLinkedList(IEnumerable<IState> collection) : base(collection)
        {

        }

        public bool Has(string stateName)
        {
            return TryGet(s => s.Name == stateName, out _);
        }

        public bool Has(Func<IState, bool> predicate)
        {
            return TryGet(predicate, out _);
        }

        public IState Get(string stateName)
        {
            if (TryGet(stateName, out var state))
            {
                return state!;
            }

            throw new ApNotFindException<StateLinkedList>($"Can't find '{stateName}' of state in StateLikedList", this);
        }

        public bool TryGet(string stateName, out IState? state)
        {
            return TryGet(s => s.Name == stateName, out state);
        }

        public bool TryGet(Func<IState, bool> predicate, out IState? destination)
        {
            foreach (var item in this)
            {
                if (predicate.Invoke(item))
                {
                    destination = item;
                    return true;
                }

                switch (item)
                {
                    case IStateSet set:
                        if (TryGet(set, predicate, out destination)) return true;
                        break;
                    case IStateSetContainer container:
                        if (TryGet(container, predicate, out destination)) return true;
                        break;
                }
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSetContainer container, Func<IState, bool> predicate, out IState? destination)
        {
            foreach (var item in container.StateSets)
            {
                if (TryGet(item.Value, predicate, out destination)) return true;
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSet set, Func<IState, bool> predicate, out IState? destination)
        {
            return set.LinkedList.TryGet(predicate, out destination);
        }

        public List<IState> GetStateLevel(string stateName)
        {
            List<IState> level = new List<IState>();
            GetStateLevel(s => s.Name == stateName, level);
            level.Reverse();
            return level;
        }

        private bool GetStateLevel(Func<IState, bool> predicate, List<IState> level)
        {
            foreach (var item in this)
            {
                if (predicate.Invoke(item))
                {
                    level.Add(item);
                    return true;
                }

                switch (item)
                {
                    case IStateSet set:
                        if (GetStateLevel(set, predicate, level))
                        {
                            level.Add(set);
                            return true;
                        }
                        break;
                    case IStateSetContainer container:
                        if (GetStateLevel(container, predicate, level))
                        {
                            level.Add(container);
                            return true;
                        }
                        break;
                }
            }

            return false;
        }

        private bool GetStateLevel(IStateSetContainer container, Func<IState, bool> predicate, List<IState> level)
        {
            foreach (var item in container.StateSets)
            {
                if (GetStateLevel(item.Value, predicate, level)) return true;
            }

            return false;
        }

        private bool GetStateLevel(IStateSet set, Func<IState, bool> predicate, List<IState> level)
        {
            return set.LinkedList.GetStateLevel(predicate, level);
        }
    }
}
