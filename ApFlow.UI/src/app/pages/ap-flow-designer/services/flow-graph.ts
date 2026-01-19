import { inject, Injectable } from "@angular/core";
import { Graph, Node, Edge } from '@antv/x6';
import { LayoutService } from "./layout.service";
import { AddNode } from "../models/add-node";
import { LayoutNode } from "../models/layout-node";
import { StartNode } from "../models/start-node";
import { EndNode } from "../models/end-node";

@Injectable()
export class FlowGraph {
    private _graph: Graph;
    private _startNode: string;

    layoutService = inject(LayoutService);

    /**
     * 获取图实例
     */
    get graph(): Graph {
        return this._graph;
    }
    /**
     * 获取节点布局信息映射
     */
    get nodeMap(): Map<string, LayoutNode> {
        return this.layoutService.nodeMap;
    }

    init(graph: Graph) {
        this._graph = graph;
    }

    private nodeCounter = 0;


    connectWithNode(source: Node, target: Node): Edge {
        const edge = this.graph.addEdge({
            source: source.id,
            target: target.id,
        });
        return edge;
    }

    connectWithIds(sourceId: string, targetId: string): Edge {
        const edge = this.graph.addEdge({
            source: sourceId,
            target: targetId,
        });
        return edge;
    }

    /**
     * 重新布局
     */
    layout() {
        this.layoutService.layout(this.graph);
    }
}
