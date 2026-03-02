# TypeScript 类型规范快速参考指南

> **版本:** 1.0.0
> **创建日期:** 2026-03-03
> **目标**: 为开发者提供快速的类型规范参考

---

## 📚 文档目录

| 文档 | 用途 |
|------|------|
| [TYPESCRIPT_STANDARDS.md](./TYPESCRIPT_STANDARDS.md) | 完整的技术规范文档 |
| [IMPLEMENTATION_PLAN.md](./IMPLEMENTATION_PLAN.md) | 详细的实施计划 |
| [CODE_OPTIMIZATION_EXAMPLES.md](./CODE_OPTIMIZATION_EXAMPLES.md) | 代码优化示例 |

---

## 🎯 快速开始

### 1. TypeScript 编译器配置

确保 `tsconfig.json` 包含以下配置:

```json
{
  "compilerOptions": {
    "strict": true,
    "noImplicitOverride": true,
    "noPropertyAccessFromIndexSignature": true,
    "noImplicitReturns": true,
    "noFallthroughCasesInSwitch": true,
    "strictNullChecks": true,
    "strictFunctionTypes": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "forceConsistentCasingInFileNames": true
  }
}
```

### 2. 核心类型导入

```typescript
// 实用类型
import type { Nullable, DeepReadonly, PartialNullable, ArrayElement } from '@types/util.types';

// 实体基类
import type { BaseEntity, PaginationParams, PaginationResponse, ListQueryParams, ApiResponse } from '@app/models/base.entity';

// HTTP 服务
import { BaseHttpService, HttpRequestConfig } from '@app/core/base-http.service';

// 动态表单
import type { FormSchema, ValidationRule, FormData, TypedSFComponent } from '@types/delon-form.types';

// 图形类型
import type { FlowNodeType, FlowNodeProps, FlowEdgeProps } from '@app/pages/flow-graph/types/x6-custom.types';
import type { GraphEventMap, TypedGraphEventHandler } from '@app/pages/flow-graph/types/graph-events.types';
```

---

## 🔧 常用模式

### 实用类型

```typescript
// 可空类型
const user: Nullable<User> = null;

// 深度只读
const readonlyUser: DeepReadonly<User> = { ... };

// 部分可空
const partialUser: PartialNullable<User> = { name: null };

// 数组元素类型
type UserArray = User[];
type UserType = ArrayElement<UserArray>; // User
```

### 接口定义

```typescript
// 基础实体
interface User extends BaseEntity {
  name: string;
  email: string;
  age: number;
}

// 分页参数
interface UserQueryParams extends ListQueryParams {
  name?: string;
  email?: string;
  minAge?: number;
  maxAge?: number;
}

// API 响应
type UserListResponse = ApiResponse<PaginationResponse<User>>;
```

### 服务类

```typescript
@Injectable({ providedIn: 'root' })
export class UserService extends BaseHttpService {
  private readonly baseUrl = '/api/users';
  
  getList(params: UserQueryParams): Observable<PaginationResponse<User>> {
    return this.getPage<User>(`${this.baseUrl}/list`, params);
  }
  
  getDetail(id: string): Observable<User> {
    return this.get<User>(`${this.baseUrl}/${id}`);
  }
  
  create(data: Omit<User, keyof BaseEntity>): Observable<User> {
    return this.post<typeof data, User>(this.baseUrl, data);
  }
  
  update(id: string, data: Partial<User>): Observable<User> {
    return this.put<typeof data, User>(`${this.baseUrl}/${id}`, data);
  }
  
  delete(id: string): Observable<void> {
    return this.delete<void>(`${this.baseUrl}/${id}`);
  }
}
```

### 组件类

```typescript
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html'
})
export class UserListComponent extends BaseListComponent<User, UserQueryParams> {
  
  protected override initQueryParams(): UserQueryParams {
    return {
      page: 1,
      pageSize: 10,
      sortBy: 'createdAt',
      sortOrder: 'desc'
    };
  }
  
  protected override getData(params: UserQueryParams): Observable<PaginationResponse<User>> {
    return this.userService.getList(params);
  }
  
  onEdit(user: User): void {
    // 编辑逻辑
  }
  
  onDelete(user: User): void {
    // 删除逻辑
  }
}
```

---

## 🚨 常见陷阱

### 1. 避免 `any`

❌ **不推荐**
```typescript
function processData(data: any): any {
  return data.value;
}
```

✅ **推荐**
```typescript
interface Data {
  value: string;
}

function processData<T extends Data>(data: T): string {
  return data.value;
}
```

### 2. 正确处理空值

❌ **不推荐**
```typescript
const user = this.getUser();
console.log(user.name); // 可能抛出错误
```

✅ **推荐**
```typescript
const user = this.getUser();
if (user) {
  console.log(user.name);
}
```

### 3. 使用类型守卫

❌ **不推荐**
```typescript
function processValue(value: unknown) {
  if (typeof value === 'string') {
    return value.toUpperCase();
  }
  return '';
}
```

✅ **推荐**
```typescript
function isString(value: unknown): value is string {
  return typeof value === 'string';
}

function processValue(value: unknown) {
  if (isString(value)) {
    return value.toUpperCase();
  }
  return '';
}
```

### 4. 使用泛型约束

❌ **不推荐**
```typescript
function getLength(value: any): number {
  return value.length;
}
```

✅ **推荐**
```typescript
interface Lengthwise {
  length: number;
}

function getLength<T extends Lengthwise>(value: T): number {
  return value.length;
}
```

---

## 🔍 类型调试技巧

### 1. 查看类型

```typescript
// 使用 Pick 查看类型
type Debug = Pick<MyType, 'key1' | 'key2'>;

// 使用 keyof 获取所有键
type Keys = keyof MyType;

// 使用 typeof 获取值类型
type ValueOf = typeof myValue;
```

### 2. 类型推断

```typescript
// 使用 infer 推断类型
type ArrayType<T> = T extends (infer U)[] ? U : never;
type StringType<T> = T extends string ? T : never;

// 使用 ReturnType 获取返回类型
type Func = () => string;
type Result = ReturnType<Func>; // string
```

### 3. 类型断言

```typescript
// 安全的类型断言
const value = maybeValue as string;

// 类型守卫
if (isString(maybeValue)) {
  const value: string = maybeValue; // 类型自动推断
}
```

---

## 📝 代码审查检查清单

在提交代码前，请检查以下项目:

- [ ] 无 `any` 类型（除非必要）
- [ ] 所有公共 API 有类型定义
- [ ] 泛型参数有意义
- [ ] 类型断言最少化
- [ ] 类型守卫使用正确
- [ ] 单元测试包含类型断言
- [ ] 文档包含类型信息
- [ ] 无类型警告
- [ ] 无编译错误
- [ ] 空值检查完整
- [ ] 错误处理合理
- [ ] 命名一致

---

## 🎓 学习资源

### 官方文档
- [TypeScript 官方文档](https://www.typescriptlang.org/docs/)
- [Angular 类型安全最佳实践](https://angular.dev/guide/typescript-configuration)
- [AntV X6 类型定义](https://x6.antv.antgroup.com/docs/api/graph)
- [ng-zorro-antd 类型定义](https://ng.ant.design/docs/getting-started/en)
- [@delon/form 文档](https://ng-alain.com/form/getting-started)

### 推荐文章
- [TypeScript Deep Dive](https://basarat.gitbook.io/typescript/)
- [Effective TypeScript](https://effectivetypescript.com/)
- [Angular Patterns](https://angularpatterns.com/)

### 工具
- [TypeScript Playground](https://www.typescriptlang.org/play)
- [TypeSearch](https://www.typescriptlang.org/dt/search)
- [DefinitelyTyped](https://github.com/DefinitelyTyped/DefinitelyTyped)

---

## 💡 快速技巧

### 1. 快速创建接口

```typescript
// 从对象创建接口
const user = {
  id: '1',
  name: 'John',
  email: 'john@example.com'
};

type User = typeof user;
```

### 2. 可选链

```typescript
// 安全访问嵌套属性
const city = user?.address?.city;

// 安全调用方法
const result = user?.getAddress?.();
```

### 3. 空值合并

```typescript
// 提供默认值
const name = user?.name ?? 'Unknown';
const count = items?.length ?? 0;
```

### 4. 类型守卫

```typescript
// 自定义类型守卫
function isUser(value: unknown): value is User {
  return (
    typeof value === 'object' &&
    value !== null &&
    'id' in value &&
    'name' in value
  );
}
```

### 5. 映射类型

```typescript
// 部分可选
type PartialUser = Partial<User>;

// 必需
type RequiredUser = Required<Partial<User>>;

// 只读
type ReadonlyUser = Readonly<User>;

// 选择
type UserBase = Pick<User, 'id' | 'name'>;

// 排除
type UserWithoutEmail = Omit<User, 'email'>;
```

---

## 🆘 故障排除

### 问题: 类型推断失败

**解决方案**:
1. 显式声明类型
2. 使用类型断言
3. 检查泛型约束
4. 查看错误消息的详细信息

### 问题: 任何类型

**解决方案**:
1. 使用 `unknown` 代替 `any`
2. 添加类型守卫
3. 使用泛型
4. 定义接口或类型

### 问题: 空值错误

**解决方案**:
1. 使用 `| null` 或 `| undefined`
2. 添加空值检查
3. 使用可选链 `?.`
4. 使用空值合并 `??`

### 问题: 类型不兼容

**解决方案**:
1. 检查类型定义
2. 使用类型断言
3. 添加类型守卫
4. 检查泛型参数

---

## 📞 获取帮助

如果您遇到类型相关的问题:

1. 查阅本文档和相关文档
2. 查阅 TypeScript 官方文档
3. 在团队内部寻求帮助
4. 在 Stack Overflow 搜索相关问题
5. 提交 Issue 到项目仓库

---

**文档版本**: 1.0.0  
**最后更新**: 2026-03-03  
**维护者**: 开发团队
