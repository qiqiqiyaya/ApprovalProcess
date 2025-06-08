using Ap.Core.Builders;
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


        public new LinkedListNode<IState> First
        {
            get
            {
                if (Count < 2)
                {
                    throw new InvalidOperationException($"Please use {nameof(StateSetBuilder)} to create a state");
                }

                return base.First!.Next!;
            }
        }

        public IState FirstState => First.Value;

        public LinkedListNode<IState> OriginFirst
        {
            get
            {
                if (Count < 2)
                {
                    throw new InvalidOperationException($"Please use {nameof(StateSetBuilder)} to create a state");
                }

                return base.First!;
            }
        }

        public bool Has(string name)
        {
            return TryGet(s => s.Name == name, out _);
        }

        public bool Has(Func<IState, bool> predicate)
        {
            return TryGet(predicate, out _);
        }

        public IState Get(string name)
        {
            if (TryGet(name, out var state))
            {
                return state!;
            }

            throw new ApNotFindException<StateLinkedList>($"Can't find '{name}' of state in StateLikedList", this);
        }

        public bool TryGet(string name, out IState? state)
        {
            return TryGet(s => s.Name == name, out state);
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
    }
}
