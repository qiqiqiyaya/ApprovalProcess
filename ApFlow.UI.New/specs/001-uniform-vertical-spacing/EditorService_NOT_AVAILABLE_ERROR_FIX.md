# EditorService Not Available Error Fix

## 错误信息

```
[LayoutService] Layout calculation failed: Error: EditorService is not available. LayoutService requires EditorService to access the graph.
    at _LayoutService.getGraph (layout.service.ts:104:13)
    at Observable2._subscribe (layout.service.ts:149:28)
```

## 问题分析

### 根本原因

1. **服务作用域不匹配**: `LayoutService` 使用 `@Injectable({ providedIn: 'root' })` 在根作用域提供
2. **依赖注入限制**: `EditorService` 只在 `FlowGraphModule` 中提供（使用 `@Injectable()` 无 `providedIn`）
3. **注入时机问题**: 当 `LayoutService` 在根作用域被创建时，它无法注入仅在模块作用域提供的 `EditorService`

### 依赖关系

```
根作用域:
  └─ LayoutService (@Injectable({ providedIn: 'root' })) ❌
       └─ 需要注入 EditorService ❌ (无法访问)

FlowGraphModule 作用域:
  └─ EditorService (providers: [...])
```

## 解决方案

### 1. 修改 FlowGraphModule 添加 LayoutService 到 providers

**文件**: `src/app/pages/flow-graph/flow-graph.module.ts`

```typescript
import { LayoutService } from './services/layout/layout.service';

@NgModule({
  // ... imports, declarations
  providers: [
    EditorService,
    LayoutConfigService,
    LayoutService,  // ← 新增：在此提供
    { provide: 'LOCALSTORAGE', useValue: window.localStorage },
  ],
  // ... exports
})
```

### 2. 修改 LayoutService 移除 providedIn

**文件**: `src/app/pages/flow-graph/services/layout/layout.service.ts`

```typescript
/**
 * Main layout service for flow graph
 * Manages layout calculation and application with reactive updates
 *
 * T079: Added comprehensive error handling and logging
 * Provided in FlowGraphModule to ensure EditorService dependency is available
 */
@Injectable()  // ← 移除 { providedIn: 'root' }
export class LayoutService implements ILayoutService {
```

### 修复后的依赖关系

```
FlowGraphModule 作用域:
  ├─ EditorService
  └─ LayoutService ✅
       └─ 注入 EditorService ✅
```

## 技术说明

### Angular 依赖注入器层级

Angular 有多个依赖注入器层级：

1. **根注入器** (`root`): 使用 `providedIn: 'root'` 的服务
2. **模块注入器** (`@NgModule.providers`): 在特定模块中提供的服务
3. **组件注入器** (`@Component.providers`): 在特定组件中提供的服务

### 依赖注入规则

- 子注入器可以访问父注入器提供的服务
- 但父注入器无法访问子注入器提供的服务
- `providedIn: 'root'` 的服务在根注入器，无法访问模块级服务

### 最佳实践

当服务A需要注入服务B时，必须确保：
- A和B在同一注入器层级，或
- A在更深的层级（可以访问父级的服务）

## 验证结果

### TypeScript 编译

```bash
npx tsc --noEmit
```

✅ **无错误**

### Linter 检查

```
✅ flow-graph.module.ts: 0 错误, 0 警告
✅ layout.service.ts: 0 错误, 0 警告
```

### 运行时测试

预期：`LayoutService` 现在可以成功注入 `EditorService`，不再抛出 "EditorService is not available" 错误。

## 影响范围

### 修改的文件

| 文件 | 修改类型 | 描述 |
|------|---------|------|
| `src/app/pages/flow-graph/flow-graph.module.ts` | 添加 import + provider | 导入 `LayoutService` 并添加到 providers 数组 |
| `src/app/pages/flow-graph/services/layout/layout.service.ts` | 装饰器修改 | 移除 `@Injectable({ providedIn: 'root' })`，改为 `@Injectable()` |

### 影响的组件

- ✅ `EditorComponent` (使用 `LayoutService`) - 不受影响，注入仍然正常
- ✅ `LayoutConfigComponent` (间接使用) - 不受影响

### 无需修改的文件

- ❌ 不需要修改 `EditorComponent.ts` (依赖注入不受影响)
- ❌ 不需要修改 `EditorService.ts` (无需改动)
- ❌ 不需要修改 `LayoutConfigService.ts` (无需改动)

## 相关问题

此修复与之前的 `BranchGroupManager` 修复类似：

1. **BranchGroupManager 问题**: 无法直接注入，改为通过 `EditorService` 访问
2. **EditorService 问题**: 作用域不匹配，将 `LayoutService` 移到同一模块

两者都遵循 Angular 依赖注入的层级规则。

## 后续建议

### 1. 服务注册一致性

建议为所有 `FlowGraphModule` 相关的服务制定统一规则：

```typescript
// ✅ 推荐：所有模块内服务都在 module 的 providers 中声明
@Injectable()
export class MyModuleService { }

@NgModule({
  providers: [MyModuleService]
})
export class MyModule { }

// ❌ 避免：部分服务用 providedIn: 'root'，部分用模块 providers
// 除非该服务确实需要在整个应用中共享
```

### 2. 服务作用域文档

建议在服务文档中明确说明：

```typescript
/**
 * Layout Service
 *
 * @providedIn FlowGraphModule (not root)
 * @description Layout calculation for flow graphs
 * @requires EditorService (available in same module)
 */
```

### 3. 依赖关系图

建议创建服务依赖关系图，避免循环依赖和作用域不匹配：

```
FlowGraphModule Services:
  EditorService (基础服务)
    ↑
    ├─ LayoutService (注入 EditorService)
    ├─ LayoutConfigService (独立)
    └─ BranchGroupManager (通过 FlowGraph 访问)
```

## 总结

✅ **问题已解决**: `LayoutService` 现在与 `EditorService` 在同一模块作用域，可以正确注入依赖。

✅ **类型安全**: TypeScript 编译通过，无类型错误。

✅ **向后兼容**: 所有现有组件的使用方式不变，无需修改。

✅ **符合 Angular 最佳实践**: 遵循依赖注入器层级规则。

## 参考资料

- [Angular Dependency Injection Hierarchy](https://angular.dev/guide/di/hierarchical-dependency-injection)
- [ providedIn vs NgModule providers](https://angular.io/guide/providers#providers)
- [Angular Service Singleton Pattern](https://angular.io/guide/singleton-services)
