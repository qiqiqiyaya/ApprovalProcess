import { Graph } from "@antv/x6";
import { LayoutNode } from "./layout-node";

export class StartNode extends LayoutNode {
    constructor(id: string, graph: Graph) {
        super(graph);
        this.node = this.graph.addNode({
            id,
            shape: 'circle',
            width: 40,
            height: 40,
        });
    }
}
