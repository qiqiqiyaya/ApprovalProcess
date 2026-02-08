/**
 * 分支组数据结构
 */
export interface BranchGroup {
    id: string;
    startNodeId: string;
    mergeNodeId: string | null;
    /**
     * 分支索引 -> 节点ID数组
     */
    branches: Map<number, string[]>;
    /**
     * 元数据
     */
    metadata: {
        /**
         * 创建时间
         */
        created: Date;
        /**
         * 更新时间
         */
        updated: Date;
        /**
         * 分支数量
         */
        branchCount: number;
        /**
         * 最大分支索引
         */
        maxBranchIndex: number;
        [key: string]: any;
    };
}

/**
 * 分支操作结果
 */
export interface BranchOperationResult {
    success: boolean;
    group?: BranchGroup;
    affectedNodes?: string[];
    error?: string;
}

/**
 * 分支统计信息
 */
export interface BranchStats {
    totalBranches: number;
    totalNodes: number;
    branchNodeCounts: Record<number, number>;
    maxDepth: number;
    avgDepth: number;
}

/**
 * 分支验证结果
 */
export interface BranchValidation {
    isValid: boolean;
    hasStart: boolean;
    hasMerge: boolean;
    branchBalance: 'balanced' | 'unbalanced' | 'single';
    warnings: string[];
    errors: string[];
}

/**
 * 节点到分支的映射
 */
export interface NodeBranchMapping {
    groupId: string;
    branchIndex: number;
    nodeIndex: number;
}

/**
 * 分支管理器事件类型
 */
export type BranchManagerEvent =
    | 'group-created'
    | 'group-deleted'
    | 'node-added'
    | 'node-deleted'
    | 'branch-added'
    | 'branch-deleted'
    | 'merge-node-set'
    | 'groups-merged';
