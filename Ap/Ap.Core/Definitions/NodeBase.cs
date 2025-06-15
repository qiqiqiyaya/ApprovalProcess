#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace Ap.Core.Definitions
{
    public abstract class NodeBase : INode
    {
        public string Id { get; set; }
    }
}
