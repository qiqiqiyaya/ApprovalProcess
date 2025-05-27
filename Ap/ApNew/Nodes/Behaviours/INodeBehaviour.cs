namespace ApNew.Nodes.Behaviours
{
    public interface INodeBehaviour
    {
        string Trigger { get; }

        public string Destination { get; }

        /// <summary>
        /// Executing state transition
        /// </summary>
        /// <param name="stateSet"></param>
        /// <returns></returns>
        ValueTask ExecuteAsync(StateSetBase stateSet);
    }
}
