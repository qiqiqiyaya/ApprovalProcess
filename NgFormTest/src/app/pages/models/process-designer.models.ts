export interface ProcessNode {
  id: string;
  type: NodeType;
  name: string;
  position: { x: number; y: number };
  size: { width: number; height: number };
  config: NodeConfig;
  nextNodes: string[]; // 下一节点ID数组
}

export interface NodeConfig {
  approvers?: ApproverConfig[]; // 审批人配置
  conditions?: ConditionConfig[]; // 条件配置
  formSettings?: FormSettings; // 表单设置
}

export interface ApproverConfig {
  type: 'user' | 'role' | 'department' | 'variable'; // 审批人类型
  value: string; // 用户ID/角色ID/部门ID/变量名
  name: string; // 显示名称
  required?: boolean; // 是否必选
}

export interface ConditionConfig {
  id: string;
  field: string; // 表单字段
  operator: 'eq' | 'neq' | 'gt' | 'gte' | 'lt' | 'lte' | 'contains'; // 操作符
  value: any; // 比较值
  targetNode: string; // 目标节点ID
}

export interface FormSettings {
  visibleFields: string[]; // 可见字段
  requiredFields: string[]; // 必填字段
  readonlyFields: string[]; // 只读字段
}

export enum NodeType {
  START = 'start',        // 开始节点
  APPROVAL = 'approval',  // 审批节点
  CONDITION = 'condition', // 条件节点
  CC = 'cc',              // 抄送节点
  END = 'end'             // 结束节点
}

export interface ProcessDefinition {
  id?: string;
  name: string;
  description: string;
  nodes: ProcessNode[];
  edges: ProcessEdge[];
  variables: ProcessVariable[];
}

export interface ProcessEdge {
  id: string;
  source: string; // 源节点ID
  target: string; // 目标节点ID
  condition?: string; // 条件表达式
}

export interface ProcessVariable {
  name: string;
  type: 'string' | 'number' | 'boolean' | 'user' | 'department';
  defaultValue?: any;
  description: string;
}


