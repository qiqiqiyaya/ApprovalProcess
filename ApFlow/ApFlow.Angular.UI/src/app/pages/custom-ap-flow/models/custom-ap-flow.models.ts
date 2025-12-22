/**
 * 流程节点数据模型
 */
export interface FlowNode {
    /** 节点唯一标识 */
    id: string;

    /** 节点形状类型 */
    shape: string;

    /** 节点X坐标 */
    x: number;

    /** 节点Y坐标 */
    y: number;

    /** 节点显示标签 */
    label: string;

    /** 节点数据 */
    data: {
        /** 节点类型 */
        nodeType: string;

        /** 节点属性 */
        properties: {
            [key: string]: any;
        };
    };
}

/**
 * 流程边数据模型
 */
export interface FlowEdge {
    /** 边唯一标识 */
    id: string;

    /** 源节点ID */
    source: string;

    /** 目标节点ID */
    target: string;

    /** 边显示标签 */
    label?: string;

    /** 边数据 */
    data: {
        /** 边类型 */
        edgeType?: string;

        /** 边属性 */
        properties: {
            [key: string]: any;
        };
    };
}

/**
 * 流程图数据模型
 */
export interface FlowGraph {
    /** 流程名称 */
    name: string;

    /** 流程描述 */
    description: string;

    /** 节点列表 */
    nodes: FlowNode[];

    /** 边列表 */
    edges: FlowEdge[];

    /** 流程变量 */
    variables?: any[];
}

/**
 * 审批人节点属性
 */
export interface ApproverNodeProperties {
    /** 审批人类型 */
    approverType: 'specific' | 'role' | 'department' | 'initiator';

    /** 指定审批人列表 */
    approvers?: string[];

    /** 审批角色 */
    role?: string;

    /** 审批部门 */
    department?: string;

    /** 是否需要发起人审批 */
    requireInitiator?: boolean;

    /** 审批方式 */
    approvalMethod: 'any' | 'all' | 'order';

    /** 审批时限（小时） */
    timeLimit?: number;

    /** 超时处理方式 */
    timeoutAction: 'skip' | 'remind' | 'escalate';

    /** 表单字段权限 */
    formFieldPermissions?: {
        [fieldId: string]: 'read' | 'write' | 'hidden';
    };
}

/**
 * 并行审批节点属性
 */
export interface ParallelNodeProperties {
    /** 并行分支数量 */
    branchCount: number;

    /** 各分支审批人配置 */
    branches: {
        /** 分支标识 */
        id: string;

        /** 分支名称 */
        name: string;

        /** 分支条件 */
        condition?: string;

        /** 审批人配置 */
        approvers: {
            approverType: 'specific' | 'role' | 'department';
            approvers?: string[];
            role?: string;
            department?: string;
        }[];

        /** 审批方式 */
        approvalMethod: 'any' | 'all';

        /** 审批时限（小时） */
        timeLimit?: number;
    }[];

    /** 合并条件 */
    mergeCondition: 'all' | 'any' | 'custom';

    /** 自定义合并条件 */
    customMergeCondition?: string;
}

/**
 * 抄送节点属性
 */
export interface CcNodeProperties {
    /** 抄送人类型 */
    ccType: 'specific' | 'role' | 'department' | 'field';

    /** 指定抄送人列表 */
    ccUsers?: string[];

    /** 抄送角色 */
    ccRole?: string;

    /** 抄送部门 */
    ccDepartment?: string;

    /** 表单字段作为抄送人 */
    ccField?: string;

    /** 抄送时机 */
    ccTiming: 'immediate' | 'after_approval' | 'after_rejection';

    /** 抄送内容 */
    ccContent: {
        /** 是否包含表单数据 */
        includeFormData: boolean;

        /** 是否包含审批意见 */
        includeComments: boolean;

        /** 是否包含附件 */
        includeAttachments: boolean;

        /** 自定义抄送消息 */
        customMessage?: string;
    };
}

/**
 * 条件分支节点属性
 */
export interface ConditionNodeProperties {
    /** 分支条件类型 */
    conditionType: 'form_field' | 'script' | 'approval_result';

    /** 分支列表 */
    branches: {
        /** 分支标识 */
        id: string;

        /** 分支名称 */
        name: string;

        /** 分支条件 */
        condition: {
            /** 表单字段条件 */
            fieldCondition?: {
                fieldId: string;
                operator: 'eq' | 'ne' | 'gt' | 'gte' | 'lt' | 'lte' | 'in' | 'not_in' | 'contains' | 'not_contains';
                value: any;
            };

            /** 脚本条件 */
            scriptCondition?: string;

            /** 审批结果条件 */
            approvalResultCondition?: {
                nodeId: string;
                result: 'approved' | 'rejected' | 'withdrawn';
            };
        };

        /** 分支权重（用于优先级） */
        priority?: number;

        /** 是否为默认分支 */
        isDefault?: boolean;
    }[];
}

/**
 * 条件并行节点属性
 */
export interface ConditionParallelNodeProperties {
    /** 并行数量 */
    parallelCount: number;

    /** 条件表达式 */
    condition: string;

    /** 各分支审批人配置 */
    branches: {
        /** 分支标识 */
        id: string;

        /** 分支名称 */
        name: string;

        /** 分支条件 */
        condition: string;

        /** 审批人配置 */
        approvers: {
            approverType: 'specific' | 'role' | 'department';
            approvers?: string[];
            role?: string;
            department?: string;
        }[];

        /** 审批方式 */
        approvalMethod: 'any' | 'all';

        /** 审批时限（小时） */
        timeLimit?: number;
    }[];

    /** 合并条件 */
    mergeCondition: 'all' | 'any' | 'custom';
}

/**
 * 流程实例数据模型
 */
export interface FlowInstance {
    /** 实例ID */
    id: string;

    /** 流程模板ID */
    templateId: string;

    /** 流程名称 */
    name: string;

    /** 发起人ID */
    initiatorId: string;

    /** 当前状态 */
    status: 'running' | 'completed' | 'cancelled' | 'suspended';

    /** 当前节点ID */
    currentNodeIds: string[];

    /** 已完成节点ID列表 */
    completedNodeIds: string[];

    /** 表单数据 */
    formData: {
        [fieldId: string]: any;
    };

    /** 审批记录 */
    approvalRecords: {
        /** 节点ID */
        nodeId: string;

        /** 审批人ID */
        approverId: string;

        /** 审批结果 */
        result: 'approved' | 'rejected' | 'withdrawn';

        /** 审批意见 */
        comment?: string;

        /** 审批时间 */
        timestamp: Date;
    }[];

    /** 创建时间 */
    createdAt: Date;

    /** 更新时间 */
    updatedAt: Date;

    /** 完成时间 */
    completedAt?: Date;
}

/**
 * 流程模板数据模型
 */
export interface FlowTemplate {
    /** 模板ID */
    id: string;

    /** 模板名称 */
    name: string;

    /** 模板描述 */
    description?: string;

    /** 流程图数据 */
    graphData: FlowGraph;

    /** 表单配置 */
    formConfig: {
        /** 表单字段列表 */
        fields: {
            /** 字段ID */
            id: string;

            /** 字段名称 */
            name: string;

            /** 字段类型 */
            type: 'text' | 'number' | 'date' | 'select' | 'multiselect' | 'textarea' | 'file' | 'user' | 'department';

            /** 是否必填 */
            required: boolean;

            /** 默认值 */
            defaultValue?: any;

            /** 选项列表（用于select和multiselect） */
            options?: {
                label: string;
                value: any;
            }[];

            /** 字段验证规则 */
            validation?: {
                min?: number;
                max?: number;
                pattern?: string;
                message?: string;
            };
        }[];
    };

    /** 模板状态 */
    status: 'draft' | 'active' | 'inactive';

    /** 创建人ID */
    creatorId: string;

    /** 创建时间 */
    createdAt: Date;

    /** 更新时间 */
    updatedAt: Date;
}