# Nodes目录业务描述

## 业务目的
提供审批流程设计器中使用的各种自定义审批节点组件，支持多样化的审批流程配置需求。

## 核心功能
1. 集中管理所有审批流程节点组件
2. 提供统一的节点组件命名和目录规范
3. 支持不同类型审批节点的自定义渲染和交互
4. 为流程设计器提供可拖拽、可配置的节点元素

## 输入/输出
- **输入**：节点配置数据（节点ID、名称、类型等）
- **输出**：渲染后的节点组件，支持节点事件交互

## 主要流程
1. 流程设计器从nodes目录加载所有可用节点组件
2. 用户在设计器中拖拽节点到画布
3. 节点组件根据配置数据渲染显示
4. 节点组件响应用户交互事件（点击、配置等）

## 依赖关系
- **组件**：ng-zorro-antd UI组件库
- **服务**：流程设计器服务、节点配置服务
- **模型**：审批节点数据模型

## 使用示例
```typescript
// 在流程设计器中使用节点组件
import { ParallelApprovalNodeComponent } from './nodes/parallel-approval-node/parallel-approval-node.component';
import { ParallelApprovalMergeNodeComponent } from './nodes/parallel-approval-merge-node/parallel-approval-Merge-node.component';

// 注册为自定义节点
this.graph.registerNode('parallel-approval', {
  view: ParallelApprovalNodeComponent
});
```

## 包含的节点组件
- **parallel-approval-node**：并行审批节点
- **parallel-approval-merge-node**：并行审批合并节点
- 未来将扩展更多节点类型（串行审批、条件分支、会签等）