import { NodeShape } from "../flow-graph/nodes/node-register";
import { FlowEdge } from "./flow-edge";
import { FlowNode } from "./flow-node";

export class FlowGraph {
    nodes: FlowNode[];
    edges: FlowEdge[];

    constructor(nodes: FlowNode[], edges: FlowEdge[]) {
        this.nodes = nodes;
        this.edges = edges;
    }

    /**
       * 在指定节点后添加审批节点和操作节点
       * 操作流程：operationNode -> approveNode -> newOperationNode -> nextNode
       * @param operationNode 要添加审批的操作节点
       */
    addApproveNode(operationNode: FlowNode): void {
        // 1. 查找原操作节点后的节点（目标节点）
        const outgoingEdge = this.edges.find(edge => edge.source === operationNode.id);

        if (!outgoingEdge) {
            console.warn(`节点 ${operationNode.id} 没有出边，无法添加审批节点`);
            return;
        }

        const nextNodeId = outgoingEdge.target;

        // 2. 创建新节点
        const timestamp = Date.now();
        const approveNode = new FlowNode(`approve_${timestamp}`);
        approveNode.shape = NodeShape.approve;
        approveNode.label = '审批节点';

        const newOperationNode = new FlowNode(`operation_new_${timestamp}`);
        newOperationNode.shape = NodeShape.operation;
        newOperationNode.label = '新操作节点';

        // 3. 不会存在分支
        // 4. 移除原边（operationNode -> nextNode）
        this.edges = this.edges.filter(edge =>
            !(edge.source === operationNode.id && edge.target === nextNodeId)
        );

        // 5. 添加新节点
        this.nodes.push(approveNode, newOperationNode);

        // 6. 创建新边（保持顺序）
        // operationNode -> approveNode
        this.edges.push(new FlowEdge(operationNode.id, approveNode.id));
        // approveNode -> newOperationNode
        this.edges.push(new FlowEdge(approveNode.id, newOperationNode.id));
        // newOperationNode -> nextNode
        this.edges.push(new FlowEdge(newOperationNode.id, nextNodeId));

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


    public static testGrap: FlowGraph = new FlowGraph(
        [
            new FlowNode('start'),
            new FlowNode('operation'),
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
            new FlowEdge('operation', 'start-node-1'),
            // new FlowEdge('BranchStart','ces1'),
            // new FlowEdge('ces1','ces11'),

            // new FlowEdge('BranchStart','ces2'),
            // new FlowEdge('ces2','ces22'),

            // new FlowEdge('ces11','BranchEnd'),
            // new FlowEdge('ces22','BranchEnd'),

            new FlowEdge('merge-node-1', 'end'),
        ]);
}

