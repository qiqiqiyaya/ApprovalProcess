import { GraphManagerService } from "../services/graph-manager.service";
import { IBranchGroup, IFlowGraph } from "./graph-definition";


export class BranchManager {
    public groups: Map<string, IBranchGroup>;

    private graphManager: GraphManagerService;

    constructor(graphManager: GraphManagerService) {
        this.graphManager = graphManager;
        this.groups = new Map<string, IBranchGroup>();
    }

    getGroup(groupId: string): IBranchGroup {
        const group= this.groups.get(groupId);
        if (!group) {
            throw new Error(`Group not found ${groupId}`);
        }
        return group;
    }

    /**
     * 创建新的分支组
     * @param parallelNodeId 分支开始节点ID
     * @param branches 初始分支数量，默认为2
     */
    create(parallelNodeId: string, branches: number = 2): IBranchGroup {
        const startNode = this.graphManager.getNode(parallelNodeId);
        const group: IBranchGroup = {
            id: crypto.randomUUID(),
            startNodeId: parallelNodeId,
            mergeNodeId: null,
            branches: new Map()
        };

        // 初始化每个分支为空数组
        for (let i = 0; i < branches; i++) {
            group.branches.set(i, []);
        }

        this.groups.set(group.id, group);
        startNode.branchGroupId = group.id;
        startNode.branchIndex = -1; // 开始节点不属于任何分支
        return group;
    }

    /**
     * 向指定分支添加节点
     * @param groupId 分支组ID
     * @param branchIndex 分支索引
     * @param nodeId 节点ID
     * @param afterNodeId 可选，在此节点后插入（默认为追加到末尾）
     */
    addNode(groupId: string, branchIndex: number, nodeId: string): void {
        const group = this.getGroup(groupId);
        const node = this.graphManager.getNode(nodeId);
        const branch = group.branches.get(branchIndex);
        if (!branch) throw new Error(`Branch not found ${branchIndex}`);
        // 更新节点所属信息
        node.branchGroupId = groupId;
        node.branchIndex = branchIndex;
        branch.push(nodeId);
    }

    /**
     * 设置合并节点
     */
    setMergeNode(groupId: string, mergeNodeId: string): void {
        const group = this.getGroup(groupId);
        const mergeNode = this.graphManager.getNode(mergeNodeId);
        group.mergeNodeId = mergeNodeId;
        mergeNode.branchGroupId = groupId;
        mergeNode.branchIndex = -2; // 合并节点不属于任何分支
    }

    /**
     * 删除分支组
     */
    deleteGroup(groupId: string): void {
        const group = this.getGroup(groupId);

        // 清除节点上的分支标记
        const affectedNodes: string[] = [];
        const clearNode = (nodeId: string) => {
            const node = this.graphManager.getNode(nodeId);
            if (node) {
                node.branchGroupId = undefined;
                node.branchIndex = undefined;
                affectedNodes.push(nodeId);
            }
        };

        clearNode(group.startNodeId);
        if (group.mergeNodeId) clearNode(group.mergeNodeId);

        for (const branch of group.branches.values()) {
            branch.forEach(clearNode);
        }

        this.groups.delete(groupId);
    }

}