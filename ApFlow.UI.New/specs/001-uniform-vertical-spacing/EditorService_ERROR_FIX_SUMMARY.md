# EditorService Not Available - 快速修复总结

## 问题
`LayoutService` 无法注入 `EditorService`，运行时抛出错误。

## 根本原因
- `LayoutService` 使用 `@Injectable({ providedIn: 'root' })` 在根作用域
- `EditorService` 只在 `FlowGraphModule` 中提供
- 根作用域的服务无法访问模块作用域的服务

## 解决方案

### 1. 修改 FlowGraphModule
```typescript
// 添加导入
import { LayoutService } from './services/layout/layout.service';

// 添加到 providers
providers: [
  EditorService,
  LayoutConfigService,
  LayoutService,  // ← 新增
  { provide: 'LOCALSTORAGE', useValue: window.localStorage },
]
```

### 2. 修改 LayoutService
```typescript
// 移除 { providedIn: 'root' }
@Injectable()  // ← 修改
export class LayoutService implements ILayoutService {
```

## 修改文件
| 文件 | 修改内容 |
|------|---------|
| `flow-graph.module.ts` | + 导入 `LayoutService`, + provider: `LayoutService` |
| `layout.service.ts` | `@Injectable({ providedIn: 'root' })` → `@Injectable()` |

## 验证
✅ TypeScript 编译通过
✅ Linter 检查通过
✅ 无需修改其他组件

## 原理
Angular 依赖注入器层级：
- 根注入器 (`providedIn: 'root'`) 无法访问模块级服务
- 模块注入器 (`@NgModule.providers`) 可以访问同一模块内的所有服务
- 确保 `LayoutService` 和 `EditorService` 在同一层级
