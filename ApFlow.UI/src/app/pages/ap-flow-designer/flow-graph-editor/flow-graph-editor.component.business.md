# FlowGraphEditorComponent 业务文档

## 1. 业务目的
提供基于 AntV X6 的流程图可视化编辑器，作为审批流程设计的核心画布组件。该组件负责流程图的初始化、节点创建、连接关系建立和自动布局，为用户提供直观的图形化流程设计体验。

## 2. 核心功能
- **画布初始化**：创建 X6 Graph 实例，配置网格、平移、缩放等基础交互功能
- **自定义形状注册**：注册流程图所需的自定义节点形状（审批节点、操作节点等）
- **节点创建与管理**：
  - 创建起始节点（发起人）
  - 创建操作节点（添加按钮）
  - 创建结束节点
- **节点关系建立**：建立节点之间的连接关系，形成流程链
- **自动布局**：通过 X6FlowGraph 服务实现节点的自动定位和连接
- **状态管理**：通过 X6FlowGraph 服务统一管理流程图状态

## 3. 输入/输出
### 输入
- 无直接 @Input 参数
- 通过 X6FlowGraph 服务进行状态管理和数据交互

### 输出
- 无直接 @Output 事件
- 通过 X6FlowGraph 服务提供流程图操作接口：
  - `graph`：获取 X6 Graph 实例
  - `startNode`：获取起始节点
  - `endNode`：获取结束节点
  - `currentNode`：获取当前操作节点

## 4. 主要流程
### 4.1 组件初始化流程
```
组件创建
  ↓
注册自定义形状（CustomShapeRegister.register）
  ↓
ngAfterViewInit 触发
  ↓
创建 X6 Graph 实例
  ↓
创建三个基础节点（开始、操作、结束）
  ↓
建立节点间连接关系
  ↓
初始化 X6FlowGraph 服务
  ↓
执行自动连接和布局
```

### 4.2 节点创建流程
1. **起始节点**：
   - ID: "start"
   - 类型: NodeType.Start
   - 标签: "发起人"
   - 尺寸: 使用 GraphConstant 定义的宽高

2. **操作节点**：
   - ID: "add"
   - 类型: NodeType.OperationNode
   - 形状: CustomShapeNames.addNodeBtn（自定义添加按钮）
   - 前置节点: 起始节点

3. **结束节点**：
   - ID: "end"
   - 类型: NodeType.End
   - 标签: "结束"
   - 前置节点: 操作节点

### 4.3 数据流转
- 节点数据通过 `NodeInfo` 接口存储在节点的 `data` 属性中
- NodeInfo 包含：节点类型、当前节点引用、前置节点数组、后置节点数组
- X6FlowGraph 服务通过 `establishFlowConnections()` 方法自动建立节点间的连接线
- X6FlowGraph 服务通过 `layoutFlowNodes()` 方法自动计算节点位置

## 5. 依赖关系
### 服务依赖
- **X6FlowGraph**：核心流程图服务，负责：
  - 流程图状态管理（graph、startNode、endNode、currentNode）
  - 节点点击事件监听
  - 自动连接节点（establishFlowConnections）
  - 自动布局计算（layoutFlowNodes）
  - 节点添加操作（addOperationNode、addApprovalNode）
  - 连接线管理（removeAllNextEdge、removeAllOutgoingEdges）

### 模型依赖
- **GraphConstant**：图形常量定义
  - nodeWidth: 节点宽度（120）
  - nodeHeight: 节点高度（60）
  - ySpace: 垂直间距（20）
  - xSpace: 水平间距（180）
  - startNodePosition: 起始节点位置（{x: 0, y: 0}）

- **NodeInfo**：节点信息接口
  - type: NodeType（节点类型枚举）
  - current: XNode（当前节点引用）
  - prevs: XNode[]（前置节点数组）
  - next: XNode[]（后置节点数组）

- **NodeType**：节点类型枚举
  - Start: 起始节点
  - End: 结束节点
  - OperationNode: 操作节点
  - Info: 信息节点
  - ParallelApproveNode: 并行审批节点

### 工具依赖
- **CustomShapeRegister**：自定义形状注册工具
  - 在构造函数中调用 `register(injector)` 注册所有自定义形状

- **CustomShapeNames**：自定义形状名称常量
  - addNodeBtn: 添加按钮形状名称

### 第三方库
- **@antv/x6**：流程图绘制核心库
  - Graph: 图实例
  - Node: 节点类
  - Edge: 连接线类

## 6. 交互说明
### 6.1 用户交互
- **画布操作**：
  - 支持平移（panning: true）
  - 支持缩放（mousewheel: true）
  - 网格辅助显示（grid: true）

- **节点交互**：
  - 点击节点触发 `node:click` 事件
  - 事件由 X6FlowGraph 服务的 `RegisterClickEvent()` 方法处理
  - 当前操作节点存储在 `currentNode` 属性中

### 6.2 自动化行为
- **自动连接**：节点创建后，X6FlowGraph 自动建立节点间的连接线
- **自动布局**：节点创建后，X6FlowGraph 自动计算并设置节点位置
- **居中显示**：初始化完成后，自动将流程图居中显示

## 7. 状态管理
### 7.1 组件状态
- 无本地状态，所有状态通过 X6FlowGraph 服务管理

### 7.2 服务状态（X6FlowGraph）
- `_graph`: X6 Graph 实例
- `_startNode`: 起始节点引用
- `_endNode`: 结束节点引用
- `_currentNode`: 当前操作节点引用
- `_addNodeModal`: 添加节点模态框引用

### 7.3 数据流向
```
用户操作 → X6FlowGraph 服务 → Graph 实例 → 视图更新
         ↑
         └─ 节点数据（NodeInfo）
```

## 8. 性能考虑
- 使用 `ngAfterViewInit` 而非 `ngOnInit` 确保 DOM 元素已渲染
- 通过服务集中管理状态，避免组件间频繁通信
- 使用 X6 的原生 API 进行节点操作，性能最优
- TODO: 考虑添加 ChangeDetectionStrategy.OnPush 进一步优化性能

## 9. 扩展性
- 通过 CustomShapeRegister 可轻松扩展新的节点类型
- X6FlowGraph 服务提供统一的节点操作接口，便于扩展新功能
- NodeInfo 接口设计灵活，可存储任意节点相关数据

## 10. 使用示例
```html
<!-- 基础使用 -->
<app-flow-graph-editor></app-flow-graph-editor>

<!-- 容器样式 -->
<div style="width: 100%; height: 600px;">
  <app-flow-graph-editor></app-flow-graph-editor>
</div>
```

## 11. 注意事项
- 必须在 `ngAfterViewInit` 中创建 Graph 实例，确保容器 DOM 已渲染
- 节点 ID 必须唯一，避免冲突
- 节点数据通过 `setData()` 方法存储，通过 `getData()` 方法获取
- 自定义形状必须在组件构造函数中注册，确保在使用前可用
- 组件销毁时需要清理 Graph 实例，避免内存泄漏（TODO: 添加 OnDestroy 生命周期钩子）
