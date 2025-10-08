import { Inject } from "@angular/core";
import { NodeOperationService } from "./node-operation.service";
import { Graph, Node as XNode } from '@antv/x6';

export enum NodeType {
    Start,
    End,
    Add,
    Info
}

export interface NodeDescription {
    type: NodeType;
    title?: string;

    current: XNode;
    prev?: XNode;
    next?: XNode[];
}


export class NodeMap{
    current:XNode;


}