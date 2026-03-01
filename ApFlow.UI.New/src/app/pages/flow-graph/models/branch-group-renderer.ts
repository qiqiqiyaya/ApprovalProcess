import { FlowGraph } from "./flow-graph";
import { BranchGroup } from "./flow-group";

export class BranchGroupRenderer {
    // 将 BranchGroup 转换为 X6 节点数据
    static branchGroupToNodes(flowGraph: FlowGraph,group: BranchGroup) {
        debugger;
        // 1. 起始节点
        flowGraph.nodes.push({
            id: group.startNodeId,
            shape: "parallel-approval",
            data: { type: 'parallel-start', groupId: group.id }
        });

        // 2. 分支节点
        group.branches.forEach((nodeIds, branchIndex) => {
            nodeIds.forEach((nodeId, nodeIndex) => {
                flowGraph.nodes.push({
                    id: nodeId,
                    shape: 'operation', // 或 icon-node
                    data: {
                        branchIndex,
                        nodeIndex,
                        groupId: group.id,
                        // type: this.getNodeType(nodeId)
                    }
                });
            });
        });

        // 3. 合并节点
        if (group.mergeNodeId) {
            flowGraph.nodes.push({
                id: group.mergeNodeId,
                shape: 'parallel-approval-merge',
                data: { type: 'parallel-merge', groupId: group.id }
            });
        }

        // return nodes;
    }

    // 生成边数据
    static branchGroupToEdges(flowGraph: FlowGraph,group: BranchGroup) {

        // 起始节点到各分支第一个节点
        group.branches.forEach((nodeIds, branchIndex) => {
            if (nodeIds.length > 0) {
                flowGraph.edges.push({
                    source: group.startNodeId,
                    target: nodeIds[0],
                    router:'orth',
                    data: { branchIndex, groupId: group.id }
                });
            }
        });

        // 分支内部节点连接
        group.branches.forEach((nodeIds, branchIndex) => {
            for (let i = 0; i < nodeIds.length - 1; i++) {
                flowGraph.edges.push({
                    source: nodeIds[i],
                    target: nodeIds[i + 1],
                    router:'orth',
                    data: { branchIndex, groupId: group.id }
                });
            }

            // 分支最后一个节点到合并节点
            if (nodeIds.length > 0 && group.mergeNodeId) {
                flowGraph.edges.push({
                    source: nodeIds[nodeIds.length - 1],
                    target: group.mergeNodeId,
                    router:'orth',
                    data: { branchIndex, groupId: group.id }
                });
            }
        });

    }
}