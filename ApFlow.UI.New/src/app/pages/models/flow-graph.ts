import { FlowEdge } from "./flow-edge";
import { FlowNode } from "./flow-node";

export class FlowGraph {
    nodes: FlowNode[];
    edges: FlowEdge[];


   public static testGrap: FlowGraph = {
        nodes: [
            new FlowNode('start'),
            new FlowNode('operation'),
            new FlowNode('BranchStart'),

            new FlowNode('ces1'),
            new FlowNode('ces11'),
            new FlowNode('ces2'),
            new FlowNode('ces22'),
            
            new FlowNode('BranchEnd'),
            new FlowNode('end'),
        ],
        edges:[
            new FlowEdge('start','operation'),
            new FlowEdge('operation','BranchStart'),
            new FlowEdge('BranchStart','ces1'),
            new FlowEdge('ces1','ces11'),

            new FlowEdge('BranchStart','ces2'),
            new FlowEdge('ces2','ces22'),

            new FlowEdge('ces11','BranchEnd'),
            new FlowEdge('ces22','BranchEnd'),
            
            new FlowEdge('BranchEnd','end'),
        ]
    }
}
