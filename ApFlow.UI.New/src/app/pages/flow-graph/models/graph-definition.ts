import { ComponentNode } from "./component-node";

export interface IFlowNode {
    id: string;
    shape: string;
    width?: number;
    height?: number;
    label?: string;
    data?: INodeDate;
    x?: number;
    y?: number;

    branchGroupId?: string; // 所属分支组ID
    branchIndex?: number; // 在分支组中的索引
}

export interface INodeDate {
    [key: string]: any;
    /**
     * Angular组件参数
     */
    ngArguments?: NgArguments;
}

/**
 * Angular组件参数
 */
export interface NgArguments extends ComponentNode {
    [key: string]: any;
}

/**
 * 矩形节点
 */
export interface IRectNode extends IFlowNode {
    shape: 'rect';
}

/**
 * 分支组数据结构
 */
export interface IBranchGroup {
    id: string;
    startNodeId: string;
    mergeNodeId: string | null;
    /**
     * 分支索引 -> 节点ID数组
     */
    branches: Map<number, string[]>;
}

/**
 * 合并节点数据结构
 */
export interface IFlowEdge {
    source: string;
    target: string;
    router?: string | any;
    data?: any;
}

/**
 * 流程图数据结构
 */
export interface IFlowGraph {
    nodes: IFlowNode[];
    edges: IFlowEdge[];
}