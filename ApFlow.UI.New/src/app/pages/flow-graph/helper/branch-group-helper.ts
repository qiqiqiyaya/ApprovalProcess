import { Observable, SubscriptionLike } from "rxjs";
import { NodeShape } from "../components/nodes/node-register";
import { BranchManager } from "../models/branch-group-manager";
import { IFlowNode, IBranchGroup, IFlowGraph } from "../models/graph-definition";
import { FlowEdgeHelper } from "./flow-edge-helper";
import { FlowNodeHelper } from "./flow-node-helper";
import { EventEmitter } from "@angular/core";

export class BranchGroupHelper {
    public static create(operationNode: IFlowNode, flowGraph: IFlowGraph, branchManager: BranchManager, $branchGroup?:EventEmitter<IBranchGroup>, number: number = 2): IBranchGroup {
        FlowNodeHelper.NotOperationNodeThenThrow(operationNode);
        const startId = operationNode.id;

        // 移除旧的边
        const startEdge = flowGraph.edges.find(edge => edge.source == startId);
        flowGraph.edges = flowGraph.edges.filter(edge => edge.source !== startId);

        const parallelNode = FlowNodeHelper.createTo(NodeShape.parallelApproval, flowGraph);
        FlowEdgeHelper.createTo(startId, parallelNode.id, flowGraph);
        const group = branchManager.create(parallelNode.id, number);

        // 创建分支操作节点并添加到分支组
        const branchOp1 = FlowNodeHelper.createTo(NodeShape.operation, flowGraph);
        this.fillGroup(group, branchOp1, 0);
        const branchOp2 = FlowNodeHelper.createTo(NodeShape.operation, flowGraph);
        this.fillGroup(group, branchOp2, 1);
       
        branchManager.addNode(branchOp1.branchGroupId!, branchOp1.branchIndex!, branchOp1.id);
        branchManager.addNode(branchOp2.branchGroupId!, branchOp2.branchIndex!, branchOp2.id);

        // 创建合并节点并设置到分支组
        const mergeNode = FlowNodeHelper.createTo(NodeShape.parallelApprovalMerge, flowGraph);
        branchManager.setMergeNode(group.id, mergeNode.id);

        FlowEdgeHelper.createTo(mergeNode.id, startEdge!.target, flowGraph);

        this.generateEdges(flowGraph, group);
        $branchGroup?.emit(group);
        return group;
    }

    /**
     * 将流程节点关联到指定的分支组中
     * @param group - 目标分支组
     * @param flowNode - 待关联的流程节点
     * @param index - 分支索引（可选），若未指定则自动添加到末尾
     */
    public static fillGroup(group: IBranchGroup, flowNode: IFlowNode, index?: number) {
        flowNode.branchGroupId = group.id;
        // 如果index未指定，则添加到最后
        let brIndex = index;
        if (index === undefined) {
            brIndex = group.branches.size + 1;
        }
        flowNode.branchIndex = brIndex;
    }

    /**
     * 为分支组生成所有必要的边连接
     * @param {IFlowGraph} flowGraph - 流程图实例
     * @param {IBranchGroup} group - 分支组配置
     * @returns {void}
     */
    public static generateEdges(flowGraph: IFlowGraph, group: IBranchGroup) {
        // 起始节点到各分支第一个节点
        group.branches.forEach((nodeIds, branchIndex) => {
            if (nodeIds.length > 0) {
                FlowEdgeHelper.createTo(group.startNodeId, nodeIds[0], flowGraph);
            }
        });

        // 分支内部节点连接
        group.branches.forEach((nodeIds, branchIndex) => {
            for (let i = 0; i < nodeIds.length - 1; i++) {
                FlowEdgeHelper.createTo(nodeIds[i], nodeIds[i + 1], flowGraph);
            }

            // 分支最后一个节点到合并节点
            if (nodeIds.length > 0 && group.mergeNodeId) {
                FlowEdgeHelper.createTo(nodeIds[nodeIds.length - 1], group.mergeNodeId, flowGraph);
            }
        });

    }
}
