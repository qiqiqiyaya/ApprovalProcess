import { Node as XNode } from '@antv/x6';

export enum NodeType {
    Start,
    End,
    /**
     * 操作节点
     */
    OperationNode,
    Info,
    /**
     * 并行添加审批节点
     */
    ParallelApproveNode
}

export interface NodeInfo {
    type: NodeType;
    title?: string;

    current: XNode;
    prevs?: XNode[];
    next?: XNode[];
}


export class NodeMap{
    current:XNode;
}