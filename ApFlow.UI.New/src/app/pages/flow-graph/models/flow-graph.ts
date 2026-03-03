import { AddApproveNodeHelper } from "../helper/add-approve-node-helper";
import { NodeShape } from "../components/nodes/node-register";
import { FlowEdge } from "./flow-edge";
import { FlowNode, RectNode } from "./flow-node";
import { BranchGroup } from "./flow-group";
import { BranchGroupManager } from "./branch-group-manager";
import { BranchGroupRenderer } from "./branch-group-renderer";
import { FlowNodeHelper } from "../helper/flow-node-helper";

export class FlowGraph {
    nodes: FlowNode[];
    edges: FlowEdge[];
    private groups: Map<string, BranchGroup> = new Map();
    /** 分支组管理器 */
    private branchManager: BranchGroupManager;

    constructor(nodes: FlowNode[], edges: FlowEdge[]) {
        this.nodes = nodes;
        this.edges = edges;
        this.branchManager = new BranchGroupManager(this);
    }

    /**
     * 创建一个新的节点并添加到图中
     * @param shape 节点形状
     * @returns 新的节点实例
     */
    newNode(shape: string) {
        const node = new FlowNode(shape);
        this.nodes.push(node);
        return node;
    }

    /**
     * 创建一个新的边并添加到图中
     * @param sourceId 源节点ID
     * @param targetId 目标节点ID
     * @returns 新的边实例
     */
    newEdge(sourceId: string, targetId: string) {
        const edge = new FlowEdge(sourceId, targetId);
        this.edges.push(edge);
        return edge;
    }

    /**
     * 根据节点ID查找节点
     * @param id 节点ID
     * @returns 找到的节点或null
     */
    findNodeById(id: string): FlowNode | null {
        return this.nodes.find(node => node.id === id) || null;
    }

    /**
     * Converts graph nodes to format suitable for layout engine
     * @returns Array of nodes for layout calculation
     */
    toLayoutNodes(): FlowNode[] {
        return this.nodes;
    }

    /**
     * Converts graph edges to format suitable for layout engine
     * @returns Array of edges for layout calculation
     */
    toLayoutEdges(): FlowEdge[] {
        return this.edges;
    }

    /**
       * 在指定节点后添加审批节点和操作节点
       * 操作流程：operationNode -> approveNode -> newOperationNode -> nextNode
       * @param operationNode 要添加审批的操作节点
       */
    addApproveNode(operationNode: FlowNode): void {
        FlowNodeHelper.NotOperationNodeThenThrow(operationNode);
        AddApproveNodeHelper.addApproveNode(operationNode, this);
    }

    /**
     * 创建一个新的分支组
     * @param startNodeId 分支开始节点ID
     */
    addBranch(operationNode: FlowNode): void {
        FlowNodeHelper.NotOperationNodeThenThrow(operationNode);
        const startNodeId = operationNode.id;
        // 移除旧的边
        const startEdge = this.edges.find(edge => edge.source == startNodeId);
        this.edges = this.edges.filter(edge => edge.source !== startNodeId);

        const parallelApprovalNode = this.newNode(NodeShape.parallelApproval);
        this.newEdge(startNodeId, parallelApprovalNode.id);
        const group = this.branchManager.createBranchGroup(parallelApprovalNode.id, 2);

        const branch1Op1 = this.newNode(NodeShape.operation);
        const branch2Op1 = this.newNode(NodeShape.operation);
        this.branchManager.addNodeToBranch(group.id, 0, branch1Op1.id);
        this.branchManager.addNodeToBranch(group.id, 1, branch2Op1.id);
        const mergeNode = this.newNode(NodeShape.parallelApprovalMerge);
        this.branchManager.setMergeNode(group.id, mergeNode.id);

        if (startEdge) {
            this.newEdge(mergeNode.id, startEdge.target);
        }
        BranchGroupRenderer.branchGroupToEdges(this, group);
    }

    /**
     * 创建一个新的流程图，包含开始节点、操作节点和结束节点，以及它们之间的边
     * @returns 新的流程图实例
     */
    static new() {
        const stratNode = new RectNode('start');
        const operationNode = new FlowNode(NodeShape.operation);
        const endNode = new RectNode('end');
        return new FlowGraph([
            stratNode,
            operationNode,
            endNode,
        ], [
            new FlowEdge(stratNode.id, operationNode.id),
            new FlowEdge(operationNode.id, endNode.id),
        ]);
    }

    public static testGrap: FlowGraph = new FlowGraph(
        [
            new FlowNode('start'),
            new FlowNode('operation'),
            new FlowNode('approve', NodeShape.approve),
            // new FlowNode('start-node-1'),

            // new FlowNode('ces1'),
            // new FlowNode('ces11'),
            // new FlowNode('ces2'),
            // new FlowNode('ces22'),

            // new FlowNode('merge-node-1'),
            new FlowNode('end'),
        ],
        [
            new FlowEdge('start', 'operation'),
            new FlowEdge('operation', 'approve'),
            new FlowEdge('approve', 'end'),
            // new FlowEdge('BranchStart','ces1'),
            // new FlowEdge('ces1','ces11'),

            // new FlowEdge('BranchStart','ces2'),
            // new FlowEdge('ces2','ces22'),

            // new FlowEdge('ces11','BranchEnd'),
            // new FlowEdge('ces22','BranchEnd'),

            // new FlowEdge('merge-node-1', 'end'),
        ]);
}

