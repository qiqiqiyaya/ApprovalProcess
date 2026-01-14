# ApFlow.UI 项目规则

## 一、AI 职责
- 严格遵循规范
- 创建/更新 `.business.md` 文件
- 保持代码一致性

## 二、技术栈
- **框架**：Angular 19 + TypeScript 5.7（严格模式）
- **图表**：AntV X6（流程图）
- **UI**：ng-zorro-antd 19
- **表单**：@delon/form

## 三、项目结构
src/app/pages/ # 功能模块
src/app/common/ # 共享组件/工具
src/app/models/ # 数据模型
src/app/services/ # 共享服务

## 四、命名规范
- **文件**：kebab-case（例：`approval-node.component.ts`）
- **类名**：PascalCase（例：`ApprovalNodeComponent`）
- **选择器**：`app-组件名`
- **服务**：`PascalCase` + `Service`

## 五、组件规范
1. 每个组件一个目录
2. 必须包含 `.business.md` 文件
3. 父子通信：`@Input()` / `@Output()`
4. 跨组件通信：通过 Service（BehaviorSubject/Signal）

## 六、业务文件（.business.md）
- 业务目的
- 核心功能
- 输入/输出
- 主要流程
- 依赖关系
- 使用示例

## 七、编码要点
1. 路由：惰性加载，kebab-case 路径
2. 类型：避免 `any`，用 `unknown` + `TODO`
3. 性能：`*ngFor` 加 `trackBy`
4. 设计：优先组合模式
5. 注释：说明“为何这样做”
6. 组件保存使用 standalone 为 false

## 八、AI 特别提示
1. 先确认 `.business.md`
2. 文件名必须 kebab-case
3. 保持类型安全
4. 注意性能优化
5. 复杂代码需详细注释说明