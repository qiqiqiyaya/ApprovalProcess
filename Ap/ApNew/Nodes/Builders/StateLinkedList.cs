using ApNew.Nodes.Core;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace ApNew.Nodes.Builders
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

        public bool TryGet(string state, out IState? destination)
        {
            foreach (var item in this)
            {
                if (item.State == state)
                {
                    destination = item;
                    return true;
                }

                switch (item)
                {
                    case IStateSet set:
                        if (TryGet(set, state, out destination)) return true;
                        break;
                    case IStateSetContainer container:
                        if (TryGet(container, state, out destination)) return true;
                        break;
                }
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSetContainer container, string state, out IState? destination)
        {
            foreach (var item in container.StateSets)
            {
                if (TryGet(item.Value, state, out destination)) return true;
            }

            destination = null;
            return false;
        }

        private bool TryGet(IStateSet set, string state, out IState? destination)
        {
            return set.LinkedList.TryGet(state, out destination);
        }
    }
}
