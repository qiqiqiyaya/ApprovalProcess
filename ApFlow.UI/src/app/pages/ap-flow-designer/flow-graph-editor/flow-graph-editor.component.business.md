# FlowGraphEditorComponent 业务文档

## 1. 业务目的
提供基于AntV X6的流程图可视化编辑功能，用于审批流程的图形化设计和配置。

## 2. 核心功能
- 初始化流程图画布
- 注册和使用自定义形状
- 创建和管理流程节点（开始、操作、结束节点）
- 节点间连接关系管理
- 流程图自动布局和连接

## 3. 输入/输出
### 输入
- 无直接输入参数，通过X6FlowGraph服务进行状态管理

### 输出
- 无直接输出事件，通过X6FlowGraph服务提供流程图操作接口

## 4. 主要流程
1. 组件初始化时注册自定义形状
2. 视图加载完成后创建X6 Graph实例
3. 初始化流程图节点（开始、操作、结束）
4. 设置节点间的连接关系
5. 初始化X6FlowGraph服务并配置自动连接和布局

## 5. 依赖关系
### 服务
- X6FlowGraph：流程图状态管理和操作服务

### 模型
- GraphConstant：图形常量定义
- NodeInfo、NodeType：节点信息和类型定义

### 其他
- @antv/x6：流程图绘制库
- CustomShapeRegister：自定义形状注册工具
- CustomShapeNames：自定义形状名称常量

## 6. 使用示例
```html
<app-flow-graph-editor></app-flow-graph-editor>
```
