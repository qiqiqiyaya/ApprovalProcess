using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Ap.Core.Models
{
    public class Flow : NodeBase
    {
        public List<NodeBase> Nodes { get; set; } = new();

        public FlowStatus FlowStatus { get; set; } = FlowStatus.Initial;

        /// <summary>
        /// not a deep search, but just at the current <see cref="Nodes"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public NodeBase GetTriggeredNode()
        {
            if (Nodes.Count == 0)
            {
                throw new Exception("The flow doesn't have any node. Prompt: There is no node in the initial flow.");
            }

            var nodeBase = Nodes.FirstOrDefault(x => x.IsTriggered)!;
            return nodeBase;
        }

        /// <summary>
        /// deep search
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static NodeBase GetDeepTriggeredNode(Flow flow)
        {
            if (flow.Nodes.Count == 0)
            {
                throw new Exception("The flow doesn't have any node. Prompt: There is no node in the initial flow.");
            }

            var nodeBase = flow.Nodes.FirstOrDefault(x => x.IsTriggered)!;

            switch (nodeBase)
            {
                case FlowContainer container:
                    foreach (var item in container.Flows)
                    {
                        return GetDeepTriggeredNode(item);
                    }
                    break;
            }

            return nodeBase;
        }
    }
}
