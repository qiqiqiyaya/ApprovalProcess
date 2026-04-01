import { NodeShape } from "../components/nodes/node-register";
import { IFlowGraph, IFlowNode } from "../models/graph-definition";
import { FlowEdgeHelper } from "./flow-edge-helper";

export class FlowNodeHelper {
    /**
     * 设置节点大小
     * @param flowNode 节点
     */
    public static setSize(flowNode: IFlowNode) {
        switch (flowNode.shape) {
            case NodeShape.start:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
            case NodeShape.end:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
            case NodeShape.operation:
                flowNode.width = 40;
                flowNode.height = 41;
                break;
            case NodeShape.approve:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
            case NodeShape.parallelApproval:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
            case NodeShape.parallelApprovalMerge:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
            case NodeShape.operation:
                flowNode.width = 80;
                flowNode.height = 40;
                break;
            default:
                flowNode.width = 170;
                flowNode.height = 110;
                break;
        }
    }

    /**
     * 非操作节点不能添加子分支
     */
    public static NotOperationNodeThenThrow(flowNode: IFlowNode) {
        if (flowNode.shape !== NodeShape.operation) {
            throw new Error('非操作节点不能添加子分支');
        }
    }

    public static createRect(lable: string): any {
        return this.create('rect', lable);
    }

    public static create(): any
    public static create(shape: string): any
    public static create(shape: string, lable: string): any
    public static create(shape: string, data: any): any
    public static create(shape?: string, lable?: string, data?: any): IFlowNode {
        const flowNode: IFlowNode = {
            id: crypto.randomUUID(),
            shape: 'rect',
        };
        if (shape) flowNode.shape = shape;
        if (data) {
            flowNode.data = {
                ...data,
                ngArguments: {
                    node: flowNode,
                }
            };
        } else {
            flowNode.data = {
                ngArguments: {
                    node: flowNode,
                }
            };
        }
        if (lable) flowNode.label = lable;
        this.setSize(flowNode);

        return flowNode;
    }

    public static createTo(shape: string, flowGraph: IFlowGraph): IFlowNode {
        const node: IFlowNode = this.create(shape);
        flowGraph.nodes.push(node);
        return node;
    }

    /**
     * 在指定节点后添加审批节点和操作节点
     * 操作流程：operationNode -> approveNode -> newOperationNode -> nextNode
     * @param operationNode 要添加审批的操作节点
     */
    public static addApproveNode(operationNode: IFlowNode, flowGraph: IFlowGraph) {
        this.NotOperationNodeThenThrow(operationNode);

        // 1. 查找原操作节点后的节点（目标节点）
        const outgoingEdge = flowGraph.edges.find(edge => edge.source === operationNode.id);
        if (!outgoingEdge) {
            console.warn(`节点 ${operationNode.id} 没有出边，无法添加审批节点`);
            return;
        }

        const nextNodeId = outgoingEdge.target;
        // 2. 创建新节点
        const timestamp = Date.now();
        const approveNode: IFlowNode = FlowNodeHelper.create(NodeShape.approve);
        const newOperationNode: IFlowNode = FlowNodeHelper.create(NodeShape.operation);
        // 3. 不会存在分支
        // 4. 移除原边（operationNode -> nextNode）
        flowGraph.edges = flowGraph.edges.filter(edge =>
            !(edge.source === operationNode.id && edge.target === nextNodeId)
        );
        // 5. 添加新节点
        flowGraph.nodes.push(approveNode, newOperationNode);

        // 6. 创建新边（保持顺序）
        // operationNode -> approveNode
        flowGraph.edges.push(FlowEdgeHelper.create(operationNode.id, approveNode.id));
        // approveNode -> newOperationNode
        flowGraph.edges.push(FlowEdgeHelper.create(approveNode.id, newOperationNode.id));
        // newOperationNode -> nextNode
        flowGraph.edges.push(FlowEdgeHelper.create(newOperationNode.id, nextNodeId));

        // 7. 添加节点数据记录（可选）
        approveNode.data = {
            type: 'approve',
            createdFrom: operationNode.id,
            timestamp
        };

        newOperationNode.data = {
            type: 'operation',
            createdFrom: operationNode.id,
            timestamp
        };
    }
}
