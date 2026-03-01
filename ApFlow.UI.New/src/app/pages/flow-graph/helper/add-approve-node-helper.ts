import { FlowEdge } from "../models/flow-edge";
import { FlowGraph } from "../models/flow-graph";
import { FlowNode } from "../models/flow-node";
import { NodeShape } from "../components/nodes/node-register";

export class AddApproveNodeHelper {
    /**
       * 在指定节点后添加审批节点和操作节点
       * 操作流程：operationNode -> approveNode -> newOperationNode -> nextNode
       * @param operationNode 要添加审批的操作节点
       */
    public static addApproveNode(operationNode: FlowNode, flowGraph: FlowGraph) {
        // 1. 查找原操作节点后的节点（目标节点）
        const outgoingEdge = flowGraph.edges.find(edge => edge.source === operationNode.id);
        if (!outgoingEdge) {
            console.warn(`节点 ${operationNode.id} 没有出边，无法添加审批节点`);
            return;
        }

        const nextNodeId = outgoingEdge.target;
        // 2. 创建新节点
        const timestamp = Date.now();
        const approveNode = new FlowNode(NodeShape.approve);
        const newOperationNode = new FlowNode(NodeShape.operation);
        // 3. 不会存在分支
        // 4. 移除原边（operationNode -> nextNode）
        flowGraph.edges = flowGraph.edges.filter(edge =>
            !(edge.source === operationNode.id && edge.target === nextNodeId)
        );
        // 5. 添加新节点
        flowGraph.nodes.push(approveNode, newOperationNode);

        // 6. 创建新边（保持顺序）
        // operationNode -> approveNode
        flowGraph.edges.push(new FlowEdge(operationNode.id, approveNode.id));
        // approveNode -> newOperationNode
        flowGraph.edges.push(new FlowEdge(approveNode.id, newOperationNode.id));
        // newOperationNode -> nextNode
        flowGraph.edges.push(new FlowEdge(newOperationNode.id, nextNodeId));

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
