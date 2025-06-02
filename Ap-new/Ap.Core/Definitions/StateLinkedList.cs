using Ap.Core.Builders;
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

        public bool TryGet(string name, out IState? destination)
        {
            foreach (var item in this)
            {
                if (item.Name == name)
                {
                    destination = item;
                    return true;
                }

                switch (item)
                {
                    case IStateSet set:
                        if (TryGet(set, name, out destination)) return true;
                        break;
                    case IStateSetContainer container:
                        if (TryGet(container, name, out destination)) return true;
                        break;
                }
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSetContainer container, string name, out IState? destination)
        {
            foreach (var item in container.StateSets)
            {
                if (TryGet(item.Value, name, out destination)) return true;
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSet set, string name, out IState? destination)
        {
            return set.LinkedList.TryGet(name, out destination);
        }
    }
}
