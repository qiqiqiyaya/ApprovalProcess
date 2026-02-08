import { EventEmitter } from "@angular/core";
import { BranchGroup, BranchOperationResult, BranchStats, BranchValidation, NodeBranchMapping } from "../models/flow-group";

export class BranchGroupManager {
    /**
     * 存储所有分支组
     */
    private branchGroups: Map<string, BranchGroup> = new Map();
    /**
     * 节点到分支组的映射，用于快速查找
     */
    private nodeToGroupMap: Map<string, NodeBranchMapping> = new Map();
    /**
     * 起始节点到分支组的映射
     */
    private startNodeToGroupMap: Map<string, string> = new Map();
    /**
     * 合并节点到分支组的映射
     */
    private mergeNodeToGroupMap: Map<string, string> = new Map();

    constructor() {
        // super();
    }

    //#region ==================== 创建操作 ====================
    /**
     * 创建新的分支组
     */
    createBranchGroup(startNodeId: string, metadata?: Record<string, any>): BranchOperationResult {
        try {
            // 检查起始节点是否已经属于其他分支组
            if (this.startNodeToGroupMap.has(startNodeId)) {
                return {
                    success: false,
                    error: `起始节点 ${startNodeId} 已经属于分支组 ${this.startNodeToGroupMap.get(startNodeId)}`
                };
            }

            // 检查节点是否已经是其他分支组中的节点
            if (this.nodeToGroupMap.has(startNodeId)) {
                return {
                    success: false,
                    error: `节点 ${startNodeId} 已经属于分支组 ${this.nodeToGroupMap.get(startNodeId)?.groupId}`
                };
            }

            const groupId = `branch_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;

            const group: BranchGroup = {
                id: groupId,
                startNodeId,
                mergeNodeId: null,
                branches: new Map(),
                metadata: {
                    created: new Date(),
                    updated: new Date(),
                    branchCount: 0,
                    maxBranchIndex: -1,
                    ...metadata
                }
            };

            // 保存分支组
            this.branchGroups.set(groupId, group);

            // 更新映射
            this.startNodeToGroupMap.set(startNodeId, groupId);
            this.nodeToGroupMap.set(startNodeId, {
                groupId,
                branchIndex: -1, // -1 表示起始节点
                nodeIndex: -1
            });

            // 触发事件
            // this.emit('group-created', { groupId, startNodeId });

            return {
                success: true,
                group: { ...group },
                affectedNodes: [startNodeId]
            };
        } catch (error) {
            return {
                success: false,
                error: `创建分支组失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }
    //#endregion ==================== 创建操作 ====================

    /**
     * 设置合并节点
     */
    setMergeNode(groupId: string, mergeNodeId: string): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            // 检查合并节点是否已经属于其他分支组
            if (this.mergeNodeToGroupMap.has(mergeNodeId) &&
                this.mergeNodeToGroupMap.get(mergeNodeId) !== groupId) {
                return {
                    success: false,
                    error: `合并节点 ${mergeNodeId} 已经属于其他分支组`
                };
            }

            // 检查节点是否已经是分支组中的节点（除了起始节点）
            const existingMapping = this.nodeToGroupMap.get(mergeNodeId);
            if (existingMapping && existingMapping.groupId !== groupId) {
                return {
                    success: false,
                    error: `节点 ${mergeNodeId} 已经属于其他分支组`
                };
            }

            // 移除旧的合并节点映射（如果存在）
            if (group.mergeNodeId) {
                this.mergeNodeToGroupMap.delete(group.mergeNodeId);
                this.nodeToGroupMap.delete(group.mergeNodeId);
            }

            // 设置新的合并节点
            group.mergeNodeId = mergeNodeId;
            group.metadata.updated = new Date();

            // 更新映射
            this.mergeNodeToGroupMap.set(mergeNodeId, groupId);
            this.nodeToGroupMap.set(mergeNodeId, {
                groupId,
                branchIndex: -2, // -2 表示合并节点
                nodeIndex: -1
            });

            // 触发事件
            // this.emit('merge-node-set', { groupId, mergeNodeId });

            return {
                success: true,
                group: { ...group }
            };
        } catch (error) {
            return {
                success: false,
                error: `设置合并节点失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    /**
     * 添加节点到指定分支
     */
    addNodeToBranch(
        groupId: string,
        branchIndex: number,
        nodeId: string,
        position?: 'start' | 'end' | number
    ): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            // 检查节点是否已经属于其他分支组
            if (this.nodeToGroupMap.has(nodeId)) {
                const mapping = this.nodeToGroupMap.get(nodeId)!;
                return {
                    success: false,
                    error: `节点 ${nodeId} 已经属于分支组 ${mapping.groupId} 的分支 ${mapping.branchIndex}`
                };
            }

            // 检查节点是否是起始节点或合并节点
            if (nodeId === group.startNodeId || nodeId === group.mergeNodeId) {
                return {
                    success: false,
                    error: `节点 ${nodeId} 是分支组的起始节点或合并节点`
                };
            }

            // 确保分支存在
            if (!group.branches.has(branchIndex)) {
                group.branches.set(branchIndex, []);
                group.metadata.branchCount = group.branches.size;
                group.metadata.maxBranchIndex = Math.max(group.metadata.maxBranchIndex, branchIndex);
            }

            const branchNodes = group.branches.get(branchIndex)!;
            let nodeIndex: number;

            // 处理插入位置
            if (position === 'start') {
                branchNodes.unshift(nodeId);
                nodeIndex = 0;
            } else if (position === 'end' || position === undefined) {
                branchNodes.push(nodeId);
                nodeIndex = branchNodes.length - 1;
            } else if (typeof position === 'number' && position >= 0 && position <= branchNodes.length) {
                branchNodes.splice(position, 0, nodeId);
                nodeIndex = position;
            } else {
                return {
                    success: false,
                    error: `无效的插入位置: ${position}`
                };
            }

            // 更新元数据
            group.metadata.updated = new Date();

            // 更新节点映射
            this.nodeToGroupMap.set(nodeId, {
                groupId,
                branchIndex,
                nodeIndex
            });

            // 更新后续节点的索引
            this.updateBranchNodeIndices(groupId, branchIndex);

            // 触发事件
            // this.emit('node-added', { groupId, branchIndex, nodeId, nodeIndex });

            return {
                success: true,
                group: { ...group },
                affectedNodes: [nodeId]
            };
        } catch (error) {
            return {
                success: false,
                error: `添加节点失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    // ==================== 删除操作 ====================

    /**
     * 删除分支组
     */
    deleteBranchGroup(groupId: string): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            const affectedNodes: string[] = [group.startNodeId];

            // 收集所有受影响的节点
            if (group.mergeNodeId) {
                affectedNodes.push(group.mergeNodeId);
            }

            group.branches.forEach((nodes, branchIndex) => {
                affectedNodes.push(...nodes);
            });

            // 清理所有映射
            this.startNodeToGroupMap.delete(group.startNodeId);

            if (group.mergeNodeId) {
                this.mergeNodeToGroupMap.delete(group.mergeNodeId);
            }

            group.branches.forEach((nodes, branchIndex) => {
                nodes.forEach(nodeId => {
                    this.nodeToGroupMap.delete(nodeId);
                });
            });

            // 删除分支组
            this.branchGroups.delete(groupId);

            // 触发事件
            // this.emit('group-deleted', { groupId, affectedNodes });

            return {
                success: true,
                affectedNodes
            };
        } catch (error) {
            return {
                success: false,
                error: `删除分支组失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    /**
     * 删除指定分支
     */
    deleteBranch(groupId: string, branchIndex: number): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            if (!group.branches.has(branchIndex)) {
                return {
                    success: false,
                    error: `分支 ${branchIndex} 不存在`
                };
            }

            const affectedNodes = group.branches.get(branchIndex) || [];

            // 清理节点映射
            affectedNodes.forEach(nodeId => {
                this.nodeToGroupMap.delete(nodeId);
            });

            // 删除分支
            group.branches.delete(branchIndex);
            group.metadata.branchCount = group.branches.size;

            // 更新最大分支索引
            if (branchIndex === group.metadata.maxBranchIndex) {
                group.metadata.maxBranchIndex = Math.max(...Array.from(group.branches.keys()), -1);
            }

            group.metadata.updated = new Date();

            // 触发事件
            // this.emit('branch-deleted', { groupId, branchIndex, affectedNodes });

            return {
                success: true,
                group: { ...group },
                affectedNodes
            };
        } catch (error) {
            return {
                success: false,
                error: `删除分支失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    /**
     * 从分支中删除指定节点
     */
    deleteNodeFromBranch(groupId: string, branchIndex: number, nodeId: string): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            const branchNodes = group.branches.get(branchIndex);
            if (!branchNodes) {
                return {
                    success: false,
                    error: `分支 ${branchIndex} 不存在`
                };
            }

            const nodeIndex = branchNodes.indexOf(nodeId);
            if (nodeIndex === -1) {
                return {
                    success: false,
                    error: `节点 ${nodeId} 不在分支 ${branchIndex} 中`
                };
            }

            // 删除节点
            branchNodes.splice(nodeIndex, 1);

            // 清理节点映射
            this.nodeToGroupMap.delete(nodeId);

            // 如果分支为空，可以删除该分支
            if (branchNodes.length === 0) {
                group.branches.delete(branchIndex);
                group.metadata.branchCount = group.branches.size;

                if (branchIndex === group.metadata.maxBranchIndex) {
                    group.metadata.maxBranchIndex = Math.max(...Array.from(group.branches.keys()), -1);
                }
            } else {
                // 更新后续节点的索引
                this.updateBranchNodeIndices(groupId, branchIndex);
            }

            group.metadata.updated = new Date();

            // 触发事件
            // this.emit('node-deleted', { groupId, branchIndex, nodeId });

            return {
                success: true,
                group: { ...group },
                affectedNodes: [nodeId]
            };
        } catch (error) {
            return {
                success: false,
                error: `删除节点失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    // ==================== 查询操作 ====================

    /**
     * 获取所有分支组
     */
    getAllGroups(): BranchGroup[] {
        return Array.from(this.branchGroups.values()).map(group => ({ ...group }));
    }

    /**
     * 获取指定分支组
     */
    getBranchGroup(groupId: string): BranchGroup | undefined {
        const group = this.branchGroups.get(groupId);
        return group ? { ...group } : undefined;
    }

    /**
     * 通过节点ID查找所属分支组
     */
    findGroupByNodeId(nodeId: string): BranchGroup | undefined {
        const mapping = this.nodeToGroupMap.get(nodeId);
        if (!mapping) return undefined;

        const group = this.branchGroups.get(mapping.groupId);
        return group ? { ...group } : undefined;
    }

    /**
     * 通过起始节点ID查找分支组
     */
    findGroupByStartNode(startNodeId: string): BranchGroup | undefined {
        const groupId = this.startNodeToGroupMap.get(startNodeId);
        if (!groupId) return undefined;

        return this.getBranchGroup(groupId);
    }

    /**
     * 获取分支统计信息
     */
    getBranchStats(groupId: string): BranchStats | undefined {
        const group = this.branchGroups.get(groupId);
        if (!group) return undefined;

        let totalNodes = 0;
        const branchNodeCounts: Record<number, number> = {};
        const depths: number[] = [];

        group.branches.forEach((nodes, branchIndex) => {
            const count = nodes.length;
            totalNodes += count;
            branchNodeCounts[branchIndex] = count;
            depths.push(count);
        });

        // 加上起始节点和合并节点
        if (group.startNodeId) totalNodes += 1;
        if (group.mergeNodeId) totalNodes += 1;

        const maxDepth = depths.length > 0 ? Math.max(...depths) : 0;
        const avgDepth = depths.length > 0 ? depths.reduce((a, b) => a + b, 0) / depths.length : 0;

        return {
            totalBranches: group.branches.size,
            totalNodes,
            branchNodeCounts,
            maxDepth,
            avgDepth
        };
    }

    /**
     * 验证分支组完整性
     */
    validateBranchGroup(groupId: string): BranchValidation {
        const group = this.branchGroups.get(groupId);
        if (!group) {
            return {
                isValid: false,
                hasStart: false,
                hasMerge: false,
                branchBalance: 'unbalanced',
                warnings: [],
                errors: [`分支组 ${groupId} 不存在`]
            };
        }

        const warnings: string[] = [];
        const errors: string[] = [];

        // 检查起始节点
        const hasStart = !!group.startNodeId;
        if (!hasStart) {
            errors.push('缺少起始节点');
        }

        // 检查合并节点
        const hasMerge = !!group.mergeNodeId;
        if (!hasMerge) {
            warnings.push('缺少合并节点');
        }

        // 检查分支平衡性
        const branchSizes: number[] = [];
        group.branches.forEach((nodes, branchIndex) => {
            branchSizes.push(nodes.length);
        });

        let branchBalance: 'balanced' | 'unbalanced' | 'single' = 'single';

        if (branchSizes.length > 1) {
            const firstSize = branchSizes[0];
            const allSame = branchSizes.every(size => size === firstSize);
            branchBalance = allSame ? 'balanced' : 'unbalanced';

            if (branchBalance === 'unbalanced') {
                warnings.push('分支节点数量不一致，可能导致流程问题');
            }
        }

        // 检查节点映射一致性
        const mappingErrors = this.validateNodeMappings(group);
        errors.push(...mappingErrors);

        const isValid = errors.length === 0;

        return {
            isValid,
            hasStart,
            hasMerge,
            branchBalance,
            warnings,
            errors
        };
    }

    // ==================== 更新操作 ====================

    /**
     * 重新排序分支中的节点
     */
    reorderBranchNodes(
        groupId: string,
        branchIndex: number,
        newOrder: string[]
    ): BranchOperationResult {
        try {
            const group = this.branchGroups.get(groupId);
            if (!group) {
                return {
                    success: false,
                    error: `分支组 ${groupId} 不存在`
                };
            }

            const branchNodes = group.branches.get(branchIndex);
            if (!branchNodes) {
                return {
                    success: false,
                    error: `分支 ${branchIndex} 不存在`
                };
            }

            // 验证新顺序包含相同的节点
            if (newOrder.length !== branchNodes.length ||
                !newOrder.every(nodeId => branchNodes.includes(nodeId))) {
                return {
                    success: false,
                    error: '新顺序必须包含完全相同的节点集合'
                };
            }

            // 更新分支节点顺序
            group.branches.set(branchIndex, [...newOrder]);
            group.metadata.updated = new Date();

            // 更新节点映射索引
            this.updateBranchNodeIndices(groupId, branchIndex);

            return {
                success: true,
                group: { ...group }
            };
        } catch (error) {
            return {
                success: false,
                error: `重新排序节点失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    /**
     * 合并两个分支组
     */
    mergeBranchGroups(
        sourceGroupId: string,
        targetGroupId: string,
        newStartNodeId?: string
    ): BranchOperationResult {
        try {
            const sourceGroup = this.branchGroups.get(sourceGroupId);
            const targetGroup = this.branchGroups.get(targetGroupId);

            if (!sourceGroup || !targetGroup) {
                return {
                    success: false,
                    error: `分支组 ${!sourceGroup ? sourceGroupId : targetGroupId} 不存在`
                };
            }

            // 确定新的起始节点
            const finalStartNodeId = newStartNodeId || targetGroup.startNodeId;

            // 创建新的分支组
            const mergeResult = this.createBranchGroup(finalStartNodeId, {
                mergedFrom: [sourceGroupId, targetGroupId],
                mergeDate: new Date()
            });

            if (!mergeResult.success) {
                return mergeResult;
            }

            const newGroup = mergeResult.group!;

            // 合并分支
            this.mergeBranchesIntoGroup(newGroup.id, sourceGroup);
            this.mergeBranchesIntoGroup(newGroup.id, targetGroup);

            // 设置合并节点（如果两个组都有）
            if (sourceGroup.mergeNodeId && targetGroup.mergeNodeId) {
                // 可以选择其中一个，或者创建新的
                this.setMergeNode(newGroup.id, sourceGroup.mergeNodeId);
            } else if (sourceGroup.mergeNodeId) {
                this.setMergeNode(newGroup.id, sourceGroup.mergeNodeId);
            } else if (targetGroup.mergeNodeId) {
                this.setMergeNode(newGroup.id, targetGroup.mergeNodeId);
            }

            // 删除原始分支组
            this.deleteBranchGroup(sourceGroupId);
            this.deleteBranchGroup(targetGroupId);

            // 触发事件
            // this.emit('groups-merged', {
            //     sourceGroupId,
            //     targetGroupId,
            //     newGroupId: newGroup.id
            // });

            return {
                success: true,
                group: newGroup
            };
        } catch (error) {
            return {
                success: false,
                error: `合并分支组失败: ${error instanceof Error ? error.message : String(error)}`
            };
        }
    }

    // ==================== 辅助方法 ====================

    /**
     * 更新分支节点索引
     */
    private updateBranchNodeIndices(groupId: string, branchIndex: number): void {
        const group = this.branchGroups.get(groupId);
        if (!group) return;

        const branchNodes = group.branches.get(branchIndex);
        if (!branchNodes) return;

        branchNodes.forEach((nodeId, nodeIndex) => {
            const mapping = this.nodeToGroupMap.get(nodeId);
            if (mapping) {
                mapping.nodeIndex = nodeIndex;
                this.nodeToGroupMap.set(nodeId, mapping);
            }
        });
    }

    /**
     * 验证节点映射一致性
     */
    private validateNodeMappings(group: BranchGroup): string[] {
        const errors: string[] = [];

        // 验证起始节点映射
        const startMapping = this.nodeToGroupMap.get(group.startNodeId);
        if (!startMapping || startMapping.groupId !== group.id || startMapping.branchIndex !== -1) {
            errors.push('起始节点映射不一致');
        }

        // 验证合并节点映射（如果有）
        if (group.mergeNodeId) {
            const mergeMapping = this.nodeToGroupMap.get(group.mergeNodeId);
            if (!mergeMapping || mergeMapping.groupId !== group.id || mergeMapping.branchIndex !== -2) {
                errors.push('合并节点映射不一致');
            }
        }

        // 验证分支节点映射
        group.branches.forEach((nodes, branchIndex) => {
            nodes.forEach((nodeId, nodeIndex) => {
                const mapping = this.nodeToGroupMap.get(nodeId);
                if (!mapping ||
                    mapping.groupId !== group.id ||
                    mapping.branchIndex !== branchIndex ||
                    mapping.nodeIndex !== nodeIndex) {
                    errors.push(`节点 ${nodeId} 映射不一致`);
                }
            });
        });

        return errors;
    }

    /**
     * 将分支合并到新组
     */
    private mergeBranchesIntoGroup(newGroupId: string, sourceGroup: BranchGroup): void {
        const newGroup = this.branchGroups.get(newGroupId);
        if (!newGroup) return;

        // 计算起始偏移量
        const offset = newGroup.metadata.maxBranchIndex + 1;

        sourceGroup.branches.forEach((nodes, branchIndex) => {
            const newBranchIndex = branchIndex + offset;

            // 创建新分支
            if (!newGroup.branches.has(newBranchIndex)) {
                newGroup.branches.set(newBranchIndex, []);
            }

            // 添加节点到新分支
            const newBranchNodes = newGroup.branches.get(newBranchIndex)!;

            nodes.forEach(nodeId => {
                // 检查节点是否已存在（理论上不应该，因为我们已经创建了新组）
                if (!this.nodeToGroupMap.has(nodeId)) {
                    newBranchNodes.push(nodeId);

                    // 更新映射
                    this.nodeToGroupMap.set(nodeId, {
                        groupId: newGroupId,
                        branchIndex: newBranchIndex,
                        nodeIndex: newBranchNodes.length - 1
                    });
                }
            });

            // 更新最大分支索引
            newGroup.metadata.maxBranchIndex = Math.max(
                newGroup.metadata.maxBranchIndex,
                newBranchIndex
            );
        });

        newGroup.metadata.branchCount = newGroup.branches.size;
        newGroup.metadata.updated = new Date();
    }

    /**
     * 清空所有数据（用于测试）
     */
    clear(): void {
        this.branchGroups.clear();
        this.nodeToGroupMap.clear();
        this.startNodeToGroupMap.clear();
        this.mergeNodeToGroupMap.clear();
    }

    /**
     * 获取管理器状态
     */
    getState(): {
        groupCount: number;
        totalNodes: number;
        nodeMappingCount: number;
    } {
        let totalNodes = 0;

        this.branchGroups.forEach(group => {
            totalNodes += 1; // 起始节点
            if (group.mergeNodeId) totalNodes += 1;

            group.branches.forEach(nodes => {
                totalNodes += nodes.length;
            });
        });

        return {
            groupCount: this.branchGroups.size,
            totalNodes,
            nodeMappingCount: this.nodeToGroupMap.size
        };
    }
}
