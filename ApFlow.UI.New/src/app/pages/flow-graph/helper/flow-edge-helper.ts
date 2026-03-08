import { IFlowEdge, IFlowGraph } from "../models/graph-definition";

export class FlowEdgeHelper {
    public static create(source: string, target: string, router: string = 'orth'): IFlowEdge {
        const edge: IFlowEdge = {
            source,
            target,
            router
        };
        return edge;
    }

    public static createTo(source: string, target: string, flowGraph: IFlowGraph, router: string = 'orth'): IFlowEdge {
        const edge = this.create(source, target, router);
        flowGraph.edges.push(edge);
        return edge;
    }
}
