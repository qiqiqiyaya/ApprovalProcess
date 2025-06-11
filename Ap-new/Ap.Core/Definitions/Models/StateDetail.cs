using Ap.Core.Behaviours;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Ap.Core.Definitions;

public class StateDetail(string id, string name, List<Transition> transitions)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public List<Transition> Transitions { get; } = transitions;

    public List<string> Triggers => Transitions.Select(s => s.Trigger).ToList();
}