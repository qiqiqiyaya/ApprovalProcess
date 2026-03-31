---
alwaysApply: true
description: 组件节点描述
---

# 组件节点描述
../../src/app/pages/flow-graph/components/nodes 目录下为组件节点的实现
| 节点标识                  | 节点名称         | 说明                       |
| ------------------------- | ---------------- | -------------------------- |
| `add-node-btn`            | 添加节点按钮     | -                          |
| `approve`                 | 审批节点         | -                          |
| `parallel-approval`       | 并行审批节点     | 与并行审批合并节点成对出现 |
| `parallel-approval-merge` | 并行审批合并节点 | 与并行审批节点成对出现     |

## 节点注册

**文件**: `node-register.ts`
· **方法**: `register`
· **参数**: `injector: Injector`
· **说明**: 注册自定义图形节点
```typescript
static register(injector: Injector) {
    // 注册自定义图形节点
}
```
