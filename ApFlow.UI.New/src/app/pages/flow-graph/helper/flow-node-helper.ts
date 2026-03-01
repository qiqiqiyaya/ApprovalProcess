import { NodeShape } from "../components/nodes/node-register";
import { FlowNode } from "../models/flow-node";

export class FlowNodeHelper {
    /**
     * 设置节点大小
     * @param flowNode 节点
     */
    public static setSize(flowNode: FlowNode) {
        switch (flowNode.shape) {
            case NodeShape.operation:
                flowNode.width = 40;
                flowNode.height = 41;
                break;
            case NodeShape.approve:
                flowNode.width = 170;
                flowNode.height = 142;
                break;
            case NodeShape.parallelApproval:
                flowNode.width = 170;
                flowNode.height = 142;
                break;
            case NodeShape.parallelApprovalMerge:
                flowNode.width = 170;
                flowNode.height = 100;
                break;
            default:
                flowNode.width = 80;
                flowNode.height = 40;
                break;
        }
    }

    /**
     * 非操作节点不能添加子分支
     */
    public static NotOperationNodeThenThrow(flowNode: FlowNode) {
        if (flowNode.shape !== NodeShape.operation) {
            throw new Error('非操作节点不能添加子分支');
        }
    }
}
