import { Graph, Node } from "@antv/x6";

export abstract class LayoutNode {
    id: string;
    x: number;
    y: number;
    row: number;
    column: number;  // 新增：列索引
    children: string[];
    shape: string;
    /** 入边 */
    incomingEdges: string[];
    /** 出边 */
    outgoingEdges: string[];
    /** 节点实例 */
    node: Node;

    protected graph: Graph;
    constructor(graph: Graph) {
        this.graph = graph;
    }

    // in edge action
    inEdge(inNode: LayoutNode): LayoutNode
    inEdge(inNode: LayoutNode | Node): LayoutNode {
        debugger;
        let node: Node;
        if (inNode instanceof LayoutNode) {
            node = inNode.node;
        } else {
            node = inNode;
        }
        this.graph.addEdge({
            source: node.id,
            target: this.node.id,
        });

        return this;
    }
    // out edge action
    outEdge(outNode: LayoutNode): LayoutNode
    outEdge(outNode: LayoutNode | Node): LayoutNode {
        let node: Node;
        if (outNode instanceof LayoutNode) {
            node = outNode.node;
        } else {
            node = outNode;
        }
        this.graph.addEdge({
            source: this.node.id,
            target: outNode.id,
        });

        return this;
    }
}
