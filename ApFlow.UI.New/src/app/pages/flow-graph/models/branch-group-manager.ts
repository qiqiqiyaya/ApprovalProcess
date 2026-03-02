import { BranchGroup } from "./flow-group";
import { FlowGraph } from "./flow-graph";

export class BranchGroupManager {
    private groups: Map<string, BranchGroup>;
    private graph: FlowGraph;

    constructor(graph: FlowGraph) {
        this.graph = graph;
        this.groups = graph['groups'] || new Map(); // 假设 FlowGraph 内部有 groups 属性
    }

    /**
     * 创建新的分支组
     * @param startNodeId 分支开始节点ID
     * @param branches 初始分支数量，默认为2
     */
    createBranchGroup(startNodeId: string, branches: number = 2): BranchGroup {
        const startNode = this.graph.findNodeById(startNodeId);
        if (!startNode) {
            throw new Error('Start node not found');
        }

        const groupId = `branch-group-${Date.now()}`;
        const now = new Date();

        const group: BranchGroup = {
            id: groupId,
            startNodeId,
            mergeNodeId: null,
            branches: new Map(),
            metadata: {
                created: now,
                updated: now,
                branchCount: branches,
                maxBranchIndex: branches - 1
            }
        };

        // 初始化每个分支为空数组
        for (let i = 0; i < branches; i++) {
            group.branches.set(i, []);
        }

        this.groups.set(groupId, group);
        startNode.branchGroup = groupId;
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
    addNodeToBranch(groupId: string, branchIndex: number, nodeId: string): void {
        const group = this.groups.get(groupId);
        if (!group) {
            throw new Error('Group not found');
        }

        const node = this.graph.findNodeById(nodeId);
        if (!node) {
            throw new Error('Node not found');
        }

        const branch = group.branches.get(branchIndex);
        if (!branch) {
            throw new Error('Branch index out of range');
        }

        // 更新节点所属信息
        node.branchGroup = groupId;
        node.branchIndex = branchIndex;
        branch.push(nodeId);

        group.metadata.updated = new Date();
    }

    /**
     * 设置合并节点
     */
    setMergeNode(groupId: string, mergeNodeId: string): void {
        const group = this.groups.get(groupId);
        if (!group) {
            throw new Error('Group not found');
        }

        const mergeNode = this.graph.findNodeById(mergeNodeId);
        if (!mergeNode) {
            throw new Error('Merge node not found');
        }

        group.mergeNodeId = mergeNodeId;
        mergeNode.branchGroup = groupId;
        mergeNode.branchIndex = -2; // 合并节点不属于任何分支

        group.metadata.updated = new Date();
    }

    /**
     * 删除分支组
     */
    deleteGroup(groupId: string): void {
        const group = this.groups.get(groupId);
        if (!group) {
            throw new Error('Group not found');
        }

        // 清除节点上的分支标记
        const affectedNodes: string[] = [];
        const clearNode = (nodeId: string) => {
            const node = this.graph.findNodeById(nodeId);
            if (node) {
                node.branchGroup = undefined;
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

    /**
     * T050: Validates uniform spacing within a branch group
     * Checks if all branches have consistent node counts and alignment
     * @param groupId Branch group ID
     * @param verticalSpacing Vertical spacing in pixels
     * @returns true if spacing is uniform, false otherwise
     */
    validateUniformSpacing(groupId: string, verticalSpacing: number): boolean {
        const group = this.groups.get(groupId);
        if (!group) {
            throw new Error('Group not found');
        }

        // Get all branch node counts
        const branchCounts: number[] = [];
        group.branches.forEach((nodes, index) => {
            branchCounts[index] = nodes.length;
        });

        // Check if all branches have the same number of nodes
        const firstCount = branchCounts[0];
        const hasUniformCounts = branchCounts.every(count => count === firstCount);

        if (!hasUniformCounts) {
            console.warn('Branch group has uneven branch lengths:', branchCounts);
            return false;
        }

        // Calculate expected vertical spacing for the entire group
        const expectedHeight = (firstCount + 1) * verticalSpacing; // +1 for start node

        // Verify node positions (if nodes have position data)
        let positionsAreUniform = true;
        group.branches.forEach((nodes, branchIndex) => {
            nodes.forEach((nodeId, nodeIndex) => {
                const node = this.graph.findNodeById(nodeId);
                if (node) {
                    const expectedY = (nodeIndex + 1) * verticalSpacing;
                    if (Math.abs(node.y! - expectedY) > 1) { // Allow small floating point error
                        positionsAreUniform = false;
                        console.warn(`Node ${nodeId} at y=${node.y}, expected y=${expectedY}`);
                    }
                }
            });
        });

        return hasUniformCounts && positionsAreUniform;
    }

    /**
     * T051: Gets all node IDs in a branch group
     * Includes start node, merge node, and all branch nodes
     * @param groupId Branch group ID
     * @returns Array of all node IDs in the group
     */
    getAllNodes(groupId: string): string[] {
        const group = this.groups.get(groupId);
        if (!group) {
            throw new Error('Group not found');
        }

        const allNodes: string[] = [];

        // Add start node
        allNodes.push(group.startNodeId);

        // Add merge node if exists
        if (group.mergeNodeId) {
            allNodes.push(group.mergeNodeId);
        }

        // Add all branch nodes
        group.branches.forEach((nodes) => {
            allNodes.push(...nodes);
        });

        return allNodes;
    }

    /**
     * Gets all branch groups
     * @returns Array of all branch groups
     */
    getAllGroups(): BranchGroup[] {
        return Array.from(this.groups.values());
    }

    /**
     * Finds branch group containing a specific node
     * @param nodeId Node ID to search for
     * @returns Branch group ID or undefined
     */
    findGroupByNode(nodeId: string): string | undefined {
        for (const [groupId, group] of this.groups) {
            // Check start node
            if (group.startNodeId === nodeId) {
                return groupId;
            }

            // Check merge node
            if (group.mergeNodeId === nodeId) {
                return groupId;
            }

            // Check branch nodes
            for (const nodes of group.branches.values()) {
                if (nodes.includes(nodeId)) {
                    return groupId;
                }
            }
        }

        return undefined;
    }
}