// ReSharper disable once CheckNamespace
namespace Ap.Core.Definitions;

public class StateDetail(string id, string name)
{
    public string Id { get; } = id;

    public string Name { get; } = name;
}