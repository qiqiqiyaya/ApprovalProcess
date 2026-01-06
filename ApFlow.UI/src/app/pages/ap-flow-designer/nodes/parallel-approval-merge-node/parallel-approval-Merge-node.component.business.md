# 并行审批合并节点组件业务描述

## 1. 业务目的
该组件用于在审批流程图中表示并行审批合并节点，用于合并多个并行审批路径，是并行审批流程的结束节点。

## 2. 核心功能
- 在流程图中展示并行审批合并节点图形
- 支持节点点击事件处理
- 显示节点名称和ID信息
- 提供可视化的并行审批合并标识

## 3. 输入/输出

### 输入
| 名称 | 类型 | 描述 | 默认值 |
|------|------|------|--------|
| nodeId | string | 节点唯一标识 | '' |
| nodeName | string | 节点名称 | '合并审批' |

### 输出
| 名称 | 类型 | 描述 |
|------|------|------|
| handleNodeClick | function | 节点点击事件处理函数 |

## 4. 主要流程
1. 组件初始化，设置默认节点名称和ID
2. 在流程图中渲染并行审批合并节点图形
3. 用户点击节点时触发handleNodeClick事件
4. 可根据业务需求扩展点击事件逻辑，如打开配置面板等

## 5. 依赖关系

### 服务依赖
无

### 组件依赖
- Angular Core
- CommonModule（通过父模块引入）

## 6. 使用示例

### 组件声明
```typescript
// 在模块中声明组件
import { ParallelApprovalMergeNodeComponent } from './nodes/parallel-approval-merge-node/parallel-approval-Merge-node.component';

@NgModule({
  declarations: [
    ParallelApprovalMergeNodeComponent
  ]
})
```

### 组件注册（用于X6流程图）
```typescript
// 在custom-shape-register.ts中注册为自定义图形
import { ParallelApprovalMergeNodeComponent } from './nodes/parallel-approval-merge-node/parallel-approval-Merge-node.component';

register({
  shape: 'parallel-approval-merge-node',
  width: 120,
  height: 40,
  content: ParallelApprovalMergeNodeComponent,
  injector: injector,
});
```

### 组件配置（在流程图中使用）
```javascript
// 创建并行审批合并节点
const node = graph.addNode({
  shape: 'parallel-approval-merge-node',
  x: 300,
  y: 100,
  data: {
    nodeId: 'pm-123',
    nodeName: '合并审批节点'
  }
});
```
