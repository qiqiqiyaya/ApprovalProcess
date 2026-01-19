import { Graph } from "@antv/x6";
import { LayoutNode } from "./layout-node";
import { CustomShapeNames } from "../custom-shape-names";

/**
 * 添加节点的节点
 */
export class AddNode extends LayoutNode {
    constructor(id: string, graph: Graph) {
        super(graph);

        this.node = this.graph.addNode({
            id,
            shape: CustomShapeNames.addNodeBtn,
            width: 40,
            height: 40,
        });
    }
}
