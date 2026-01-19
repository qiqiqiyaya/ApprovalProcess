import { inject } from "@angular/core";
import { FlowGraph } from "../services/flow-graph";
import { LayoutNode } from "./layout-node";
import { StartNode } from "./start-node";
import { AddNode } from "./add-node";
import { EndNode } from "./end-node";

let nodeCounter = 0;

export function newStartNode(flowGraph: FlowGraph): LayoutNode {
    const id = `node_start_${nodeCounter++}`;
    const startNode = new StartNode(id, flowGraph.graph);
    flowGraph.nodeMap.set(id, startNode);
    return startNode;
}
/**
 * 添加节点
 */
export function newAddNode(flowGraph: FlowGraph): LayoutNode {
    const id = `node_Add_${nodeCounter++}`;
    const addNode = new AddNode(id, flowGraph.graph);
    flowGraph.nodeMap.set(id, addNode);
    return addNode;
}
/**
 * 结束节点
 */
export function newEndNode(flowGraph: FlowGraph): LayoutNode {
    const id = `node_end_${nodeCounter++}`;
    const endNode = new EndNode(id, flowGraph.graph);
    flowGraph.nodeMap.set(id, endNode);
    return endNode;
}