# ApFlow.UI TypeScript 类型规范文档

> **版本:** 1.0.0
> **创建日期:** 2026-03-03
> **适用框架:** Angular 19+ | TypeScript 5.7+ | ng-zorro-antd | @delon/form | AntV X6

---

## 📋 目录

1. [概述](#概述)
2. [技术栈选择总结](#技术栈选择总结)
3. [TypeScript 编译器配置](#typescript-编译器配置)
4. [核心类型系统](#核心类型系统)
5. [Angular 组件类型规范](#angular-组件类型规范)
6. [动态表单类型系统](#动态表单类型系统)
7. [AntV X6 图形类型集成](#antv-x6-图形类型集成)
8. [业务组件封装规范](#业务组件封装规范)
9. [RxJS 类型安全](#rxjs-类型安全)
10. [依赖注入类型策略](#依赖注入类型策略)
11. [测试类型规范](#测试类型规范)
12. [实施路线图](#实施路线图)

---

## 概述

本文档定义了 ApFlow.UI 项目的 TypeScript 类型规范，旨在确保：
- **类型安全**: 在编译期捕获尽可能多的错误
- **可维护性**: 清晰的类型定义和文档注释
- **可扩展性**: 支持业务需求变更的灵活类型设计
- **团队协作**: 统一的代码风格和最佳实践

---

## 技术栈选择总结

基于项目需求分析，以下是各领域的技术选择：

### 1. TypeScript 编译器选项
**选择: 严格模式 (Strict Mode)**

```json
{
  "strict": true,
  "noImplicitOverride": true,
  "noPropertyAccessFromIndexSignature": true,
  "noImplicitReturns": true,
  "noFallthroughCasesInSwitch": true
}
```

**理由:**
- 提供最大程度的类型安全保障
- 在编译期捕获潜在的运行时错误
- 提升代码质量和可维护性
- 已在项目中配置并应用

---

### 2. 表单类型系统
**选择: 泛型包装器 + 类型推断**

**理由:**
- 避免手动类型断言
- 通过泛型绑定表单控件与数据模型类型
- 支持复杂的嵌套表单场景
- 提升开发体验和类型安全

---

### 3. @delon/form 类型定义
**选择: 泛型参数扩展**

**理由:**
- 为 `SFComponent` 定义泛型参数，传递 schema 和 form data 类型
- 确保动态表单的类型完整性
- 支持自定义小部件的类型扩展
- 提供完整的类型推断支持

---

### 4. ng-zorro-antd 类型增强
**选择: 业务组件封装 + 类型安全**

**理由:**
- 确保 UI 一致性
- 业务逻辑复用
- 提供完整的类型安全支持
- 符合企业级应用最佳实践

---

### 5. AntV X6 图形类型集成
**选择: 声明合并 + 强类型 Props**

**理由:**
- 利用声明合并增强 X6 的类型定义
- 为自定义节点/边定义强类型的 props 和事件
- 补充缺失的类型定义
- 提供完整的类型推断支持

---

### 6. RxJS 类型安全
**选择: 类型推断优先 + 公共 API 边界显式类型**

**理由:**
- 优先使用类型推断（如 RxJS 管道的 `pipe` 链）
- 在公共 API 边界处显式声明类型
- 避免 `any` 类型污染
- 提升代码可读性和可维护性

---

### 7. 依赖注入类型策略
**选择: 混合使用(无统一策略)**

**理由:**
- 根据具体场景选择合适的策略
- 简单场景使用 `@Injectable()` 无类型约束
- 复杂场景使用 `InjectionToken<T>` 配合泛型类型约束
- 平衡类型安全性和开发效率

---

## TypeScript 编译器配置

### 当前配置分析

```json
// tsconfig.json
{
  "compilerOptions": {
    "strict": true,                                    // ✅ 启用所有严格类型检查
    "noImplicitOverride": true,                        // ✅ 要求显式 override 标记
    "noPropertyAccessFromIndexSignature": true,       // ✅ 禁止索引签名属性访问
    "noImplicitReturns": true,                        // ✅ 要求函数所有路径有返回值
    "noFallthroughCasesInSwitch": true,               // ✅ 禁止 switch 穿透
    "strictPropertyInitialization": false,            // ⚠️ 允许未初始化属性
    "skipLibCheck": true,                             // ✅ 跳过库文件检查
    "isolatedModules": true,                          // ✅ 独立模块编译
    "experimentalDecorators": true,                   // ✅ 启用装饰器
    "target": "ES2022",                               // ✅ 现代语法
    "module": "preserve"                              // ✅ 保留模块格式
  },
  "angularCompilerOptions": {
    "strictInjectionParameters": true,                 // ✅ 严格注入参数检查
    "strictInputAccessModifiers": true,                // ✅ 严格输入访问修饰符
    "strictTemplates": true                            // ✅ 严格模板检查
  }
}
```

### 推荐优化

```json
{
  "compilerOptions": {
    // 现有配置保持不变
    "strict": true,
    "noImplicitOverride": true,
    "noPropertyAccessFromIndexSignature": true,
    "noImplicitReturns": true,
    "noFallthroughCasesInSwitch": true,
    
    // 新增推荐配置
    "strictNullChecks": true,                          // ✅ 严格空值检查
    "strictFunctionTypes": true,                       // ✅ 严格函数类型检查
    "noUnusedLocals": true,                           // ✅ 检测未使用的局部变量
    "noUnusedParameters": true,                        // ✅ 检测未使用的参数
    "forceConsistentCasingInFileNames": true,          // ✅ 强制文件名大小写一致
    
    // 路径映射
    "baseUrl": "./",
    "paths": {
      "@core/*": ["src/app/core/*"],
      "@shared/*": ["src/app/shared/*"],
      "@business/*": ["src/app/pages/business/*"],
      "@flow-graph/*": ["src/app/pages/flow-graph/*"],
      "@models/*": ["src/app/models/*"],
      "@services/*": ["src/app/services/*"]
    },
    
    // 类型声明文件
    "typeRoots": [
      "./node_modules/@types",
      "./src/types"
    ]
  }
}
```

---

## 核心类型系统

### 实用类型定义

```typescript
// src/types/util.types.ts

/**
 * 可空类型 - 允许 null 或 undefined
 */
export type Nullable<T> = T | null | undefined;

/**
 * 深度只读类型
 */
export type DeepReadonly<T> = {
  readonly [P in keyof T]: T[P] extends object ? DeepReadonly<T[P]> : T[P];
};

/**
 * 部分可空类型 - 所有属性变为可选
 */
export type PartialNullable<T> = {
  [P in keyof T]?: Nullable<T[P]>;
};

/**
 * 提取数组的元素类型
 */
export type ArrayElement<T> = T extends (infer U)[] ? U : never;

/**
 * 函数参数类型提取
 */
export type FunctionParameters<T> = T extends (...args: infer P) => any ? P : never;

/**
 * 函数返回值类型提取
 */
export type FunctionReturnType<T> = T extends (...args: any[]) => infer R ? R : never;

/**
 * 排除指定属性的类型
 */
export type OmitByType<T, U> = Omit<T, { [K in keyof T]: T[K] extends U ? K : never }[keyof T]>;

/**
 * 保留指定属性的类型
 */
export type PickByType<T, U> = Pick<T, { [K in keyof T]: T[K] extends U ? K : never }[keyof T]>;

/**
 * 字符串字面量类型转联合类型
 */
export type StringLiteralToUnion<T> = T extends `${infer U}` ? U : never;

/**
 * 对象值类型转联合类型
 */
export type ValueOf<T> = T[keyof T];

/**
 * 深度递归类型
 */
export type DeepPartial<T> = {
  [P in keyof T]?: T[P] extends object ? DeepPartial<T[P]> : T[P];
};

/**
 * 必需属性类型
 */
export type RequiredKeys<T> = {
  [K in keyof T]-?: {} extends Pick<T, K> ? never : K;
}[keyof T];

/**
 * 可选属性类型
 */
export type OptionalKeys<T> = {
  [K in keyof T]-?: {} extends Pick<T, K> ? K : never;
}[keyof T];
```

### 实体基类定义

```typescript
// src/app/models/base.entity.ts

/**
 * 实体基类 - 所有业务实体的基础类型
 */
export interface BaseEntity {
  /** 实体唯一标识 */
  id: string;
  /** 创建时间 */
  createdAt: Date;
  /** 更新时间 */
  updatedAt: Date;
  /** 创建人ID */
  createdBy?: string;
  /** 更新人ID */
  updatedBy?: string;
  /** 是否删除 */
  isDeleted?: boolean;
}

/**
 * 分页参数
 */
export interface PaginationParams {
  /** 当前页码，从1开始 */
  page: number;
  /** 每页数量 */
  pageSize: number;
  /** 排序字段 */
  sortBy?: string;
  /** 排序方向 */
  sortOrder?: 'asc' | 'desc';
}

/**
 * 分页响应
 */
export interface PaginationResponse<T> {
  /** 数据列表 */
  data: T[];
  /** 总记录数 */
  total: number;
  /** 当前页码 */
  page: number;
  /** 每页数量 */
  pageSize: number;
  /** 总页数 */
  totalPages: number;
}

/**
 * API 响应包装
 */
export interface ApiResponse<T = void> {
  /** 是否成功 */
  success: boolean;
  /** 响应数据 */
  data?: T;
  /** 错误信息 */
  message?: string;
  /** 错误码 */
  code?: string;
}

/**
 * 列表查询参数基类
 */
export interface ListQueryParams extends PaginationParams {
  /** 搜索关键词 */
  keyword?: string;
  /** 开始日期 */
  startDate?: Date;
  /** 结束日期 */
  endDate?: Date;
  /** 状态过滤 */
  status?: string;
}
```

---

## Angular 组件类型规范

### 泛型基类组件

```typescript
// src/app/core/base-list.component.ts

import { OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { debounceTime, takeUntil } from 'rxjs/operators';
import { PaginationParams, PaginationResponse, ListQueryParams } from '../models/base.entity';

/**
 * 列表组件基类 - 提供通用的列表查询功能
 * @template T 实体类型
 * @template Q 查询参数类型
 */
export abstract class BaseListComponent<T, Q extends ListQueryParams = ListQueryParams> 
  implements OnInit, OnDestroy {
  
  /** 数据列表 */
  dataList: T[] = [];
  
  /** 加载状态 */
  loading = false;
  
  /** 总记录数 */
  total = 0;
  
  /** 当前页码 */
  pageIndex = 1;
  
  /** 每页数量 */
  pageSize = 10;
  
  /** 查询参数 */
  queryParams!: Q;
  
  /** 销毁信号 */
  private destroy$ = new Subject<void>();
  
  /**
   * 子类实现 - 获取数据源
   * @param params 查询参数
   * @returns 数据列表Observable
   */
  protected abstract getData(params: Q): Observable<PaginationResponse<T>>;
  
  /**
   * 子类实现 - 初始化查询参数
   * @returns 查询参数实例
   */
  protected abstract initQueryParams(): Q;
  
  ngOnInit(): void {
    this.queryParams = this.initQueryParams();
    this.loadPageData();
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  
  /**
   * 加载分页数据
   */
  loadPageData(): void {
    this.loading = true;
    this.getData(this.queryParams)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.loading = false)
      )
      .subscribe(response => {
        this.dataList = response.data;
        this.total = response.total;
      });
  }
  
  /**
   * 搜索
   * @param params 查询参数
   */
  search(params: Partial<Q>): void {
    this.queryParams = { ...this.queryParams, ...params, page: 1 };
    this.loadPageData();
  }
  
  /**
   * 分页变化
   * @param page 页码
   * @param pageSize 每页数量
   */
  onPageIndexChange(page: number, pageSize: number): void {
    this.queryParams.page = page;
    this.queryParams.pageSize = pageSize;
    this.loadPageData();
  }
}
```

### 强类型组件输入输出

```typescript
// src/app/pages/flow-graph/components/editor/editor.component.ts

import { Component, ElementRef, Injector, OnInit, viewChild } from '@angular/core';
import { Graph, Snapline } from '@antv/x6';
import { DagreLayout } from '@antv/layout';
import { NodeRegister, NodeShape } from '../nodes/node-register';
import { FlowGraph } from '../../models/flow-graph';
import { EditorService } from '../../services/editor.service';

/**
 * 编辑器组件配置接口
 */
export interface EditorComponentConfig {
  /** 是否启用网格 */
  enableGrid?: boolean;
  /** 是否启用拖拽 */
  enablePanning?: boolean;
  /** 是否启用滚轮缩放 */
  enableMousewheel?: boolean;
  /** 是否启用对齐线 */
  enableSnapline?: boolean;
  /** 背景颜色 */
  backgroundColor?: string;
}

/**
 * 编辑器事件类型
 */
export interface EditorEvents {
  /** 节点添加事件 */
  nodeAdded: { nodeId: string; nodeType: string };
  /** 节点删除事件 */
  nodeRemoved: { nodeId: string };
  /** 边添加事件 */
  edgeAdded: { edgeId: string; sourceId: string; targetId: string };
  /** 选择变化事件 */
  selectionChanged: { selectedIds: string[] };
}

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit {
  
  /** 图形实例 */
  private graph!: Graph;
  
  /** 容器引用 */
  private container = viewChild.required<ElementRef>('graphContainer');
  
  /** 配置项 */
  @Input() config: EditorComponentConfig = {
    enableGrid: true,
    enablePanning: true,
    enableMousewheel: true,
    enableSnapline: true,
    backgroundColor: '#f5f5f5'
  };
  
  /** 事件输出 */
  @Output() nodeAdded = new EventEmitter<EditorEvents['nodeAdded']>();
  @Output() nodeRemoved = new EventEmitter<EditorEvents['nodeRemoved']>();
  @Output() edgeAdded = new EventEmitter<EditorEvents['edgeAdded']>();
  @Output() selectionChanged = new EventEmitter<EditorEvents['selectionChanged']>();
  
  constructor(
    private injector: Injector,
    private editorService: EditorService
  ) {}
  
  ngOnInit(): void {
    this.initializeGraph();
    this.setupEventListeners();
  }
  
  /**
   * 初始化图形实例
   */
  private initializeGraph(): void {
    this.graph = new Graph({
      container: this.container().nativeElement,
      grid: this.config.enableGrid ? { size: 10, visible: true } : false,
      panning: this.config.enablePanning,
      mousewheel: this.config.enableMousewheel ? { enabled: true } : false,
      background: {
        color: this.config.backgroundColor
      }
    });
    
    if (this.config.enableSnapline) {
      this.graph.use(new Snapline({ enabled: true }));
    }
    
    // 注册节点类型
    NodeRegister.register(this.injector);
    
    // 设置服务
    this.editorService.setGraph(this.graph);
    this.editorService.setFlowGraph(FlowGraph.new());
    this.editorService.renderGraph();
  }
  
  /**
   * 设置事件监听
   */
  private setupEventListeners(): void {
    this.graph.on('node:added', ({ node }) => {
      this.nodeAdded.emit({
        nodeId: node.id,
        nodeType: node.shape
      });
    });
    
    this.graph.on('node:removed', ({ node }) => {
      this.nodeRemoved.emit({ nodeId: node.id });
    });
    
    this.graph.on('edge:added', ({ edge }) => {
      this.edgeAdded.emit({
        edgeId: edge.id,
        sourceId: edge.source.cellId,
        targetId: edge.target.cellId
      });
    });
    
    this.graph.on('selection:changed', ({ added, removed }) => {
      const selectedIds = [
        ...added.map(cell => cell.id),
        ...this.graph.getSelectedCells().map(cell => cell.id)
      ];
      this.selectionChanged.emit({ selectedIds });
    });
  }
}
```

---

## 动态表单类型系统

### @delon/form 泛型扩展

```typescript
// src/types/delon-form.types.ts

import { SFComponent, SFValue } from '@delon/form';

/**
 * 表单 Schema 类型
 */
export interface FormSchema<T = any> {
  /** 字段类型 */
  type: string;
  /** 字段键名 */
  key: string;
  /** 字段标题 */
  title?: string;
  /** 默认值 */
  defaultValue?: T;
  /** 是否必填 */
  required?: boolean;
  /** 验证规则 */
  rules?: ValidationRule[];
  /** 自定义配置 */
  ui?: Record<string, any>;
  /** 枚举选项 */
  enum?: Array<{ label: string; value: T }>;
  /** 条件显示 */
  visibleIf?: Record<string, any>;
  /** 条件禁用 */
  disabledIf?: Record<string, any>;
}

/**
 * 验证规则
 */
export interface ValidationRule {
  /** 验证类型 */
  type: 'required' | 'pattern' | 'minLength' | 'maxLength' | 'min' | 'max' | 'custom';
  /** 验证消息 */
  message?: string;
  /** 验证值 */
  value?: any;
  /** 自定义验证函数 */
  validator?: (value: any, formData: any) => boolean;
}

/**
 * 表单数据类型
 */
export interface FormData<T extends Record<string, any>> {
  [K in keyof T]: T[K];
}

/**
 * 泛型 SFComponent 扩展
 */
export type TypedSFComponent<TData extends Record<string, any>, TScheme extends Record<string, FormSchema>> = SFComponent & {
  /** 类型化的表单数据 */
  formData: FormData<TData>;
  /** 类型化的 Schema */
  schema: TScheme;
  /** 类型化的值获取 */
  getValue: (key: keyof TData) => any;
  /** 类型化的值设置 */
  setValue: (key: keyof TData, value: any) => void;
  /** 类型化的表单重置 */
  reset: (data?: Partial<FormData<TData>>) => void;
  /** 类型化的表单验证 */
  validate: (onlyDirty?: boolean) => Promise<Record<keyof TData, any>>;
};

/**
 * 动态表单组件基类
 */
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

export abstract class BaseDynamicFormComponent<
  TData extends Record<string, any>,
  TScheme extends Record<string, FormSchema>
> implements OnInit {
  
  /** 表单实例 */
  @ViewChild(SFComponent) sf!: TypedSFComponent<TData, TScheme>;
  
  /** 表单 Schema */
  @Input() schema!: TScheme;
  
  /** 表单数据 */
  @Input() formData!: Partial<TData>;
  
  /** 表单提交事件 */
  @Output() submit = new EventEmitter<TData>();
  
  /** 表单值变化事件 */
  @Output() valueChange = new EventEmitter<Partial<TData>>();
  
  /** 是否正在提交 */
  submitting = false;
  
  ngOnInit(): void {
    if (!this.schema) {
      throw new Error('Schema is required');
    }
  }
  
  /**
   * 获取类型化的表单值
   */
  getFormData(): FormData<TData> {
    return this.sf?.getValue() as FormData<TData>;
  }
  
  /**
   * 设置类型化的表单值
   */
  setFormData(data: Partial<TData>): void {
    this.sf?.setValue(data as any);
  }
  
  /**
   * 重置表单
   */
  resetForm(data?: Partial<TData>): void {
    this.sf?.reset(data as any);
  }
  
  /**
   * 验证表单
   */
  async validateForm(): Promise<boolean> {
    const errors = await this.sf?.validate();
    return Object.keys(errors || {}).length === 0;
  }
  
  /**
   * 提交表单
   */
  async handleSubmit(): Promise<void> {
    if (this.submitting) return;
    
    const isValid = await this.validateForm();
    if (!isValid) return;
    
    this.submitting = true;
    try {
      const data = this.getFormData();
      await this.onSubmit(data);
      this.submit.emit(data);
    } finally {
      this.submitting = false;
    }
  }
  
  /**
   * 子类实现 - 提交逻辑
   */
  protected abstract onSubmit(data: TData): Promise<void> | void;
  
  /**
   * 值变化处理
   */
  onValueChange($event: SFValue): void {
    this.valueChange.emit($event.value as Partial<TData>);
  }
}
```

### 实际使用示例

```typescript
// src/app/pages/business/create/create.component.ts

import { Component } from '@angular/core';
import { BaseDynamicFormComponent } from '@core/base-dynamic-form.component';
import { FormSchema } from '@types/delon-form.types';

/**
 * 审批流程表单数据
 */
interface ApprovalProcessFormData {
  /** 流程名称 */
  name: string;
  /** 流程描述 */
  description: string;
  /** 审批类型 */
  approvalType: 'single' | 'multi' | 'parallel';
  /** 审批步骤数 */
  stepCount: number;
  /** 是否启用 */
  enabled: boolean;
}

/**
 * 表单 Schema 定义
 */
const approvalProcessSchema: Record<string, FormSchema> = {
  name: {
    type: 'string',
    title: '流程名称',
    required: true,
    rules: [
      { type: 'required', message: '流程名称不能为空' },
      { type: 'minLength', value: 2, message: '流程名称至少2个字符' },
      { type: 'maxLength', value: 50, message: '流程名称最多50个字符' }
    ]
  },
  description: {
    type: 'textarea',
    title: '流程描述'
  },
  approvalType: {
    type: 'select',
    title: '审批类型',
    required: true,
    defaultValue: 'single',
    enum: [
      { label: '单人审批', value: 'single' },
      { label: '多人审批', value: 'multi' },
      { label: '并行审批', value: 'parallel' }
    ]
  },
  stepCount: {
    type: 'number',
    title: '审批步骤数',
    ui: { min: 1, max: 10, step: 1 }
  },
  enabled: {
    type: 'boolean',
    title: '是否启用',
    defaultValue: true
  }
};

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent extends BaseDynamicFormComponent<
  ApprovalProcessFormData,
  typeof approvalProcessSchema
> {
  
  /** 表单 Schema */
  schema = approvalProcessSchema;
  
  /** 初始表单数据 */
  formData: Partial<ApprovalProcessFormData> = {
    approvalType: 'single',
    enabled: true
  };
  
  /**
   * 提交表单
   */
  protected async onSubmit(data: ApprovalProcessFormData): Promise<void> {
    console.log('提交数据:', data);
    // 调用服务保存数据
  }
}
```

---

## AntV X6 图形类型集成

### 自定义节点类型定义

```typescript
// src/app/pages/flow-graph/types/x6-custom.types.ts

import { Node, Edge } from '@antv/x6';

/**
 * 节点类型字面量
 */
export type FlowNodeType = 
  | 'start'
  | 'end'
  | 'operation'
  | 'approve'
  | 'parallel-approval'
  | 'parallel-approval-merge'
  | 'condition'
  | 'sub-process';

/**
 * 自定义节点属性
 */
export interface FlowNodeProps extends Node.Properties {
  /** 节点类型 */
  nodeType: FlowNodeType;
  /** 节点标签 */
  label: string;
  /** 节点描述 */
  description?: string;
  /** 节点颜色 */
  color?: string;
  /** 是否可编辑 */
  editable?: boolean;
  /** 审批人列表 */
  approvers?: string[];
  /** 审批规则 */
  approvalRule?: string;
  /** 分支条件 */
  branchCondition?: string;
  /** 自定义数据 */
  customData?: Record<string, any>;
}

/**
 * 自定义边属性
 */
export interface FlowEdgeProps extends Edge.Properties {
  /** 边标签 */
  label?: string;
  /** 边类型 */
  edgeType?: 'default' | 'condition' | 'parallel';
  /** 分支条件 */
  condition?: string;
  /** 条件表达式 */
  expression?: string;
  /** 是否双向 */
  double?: boolean;
}

/**
 * X6 节点类型声明合并
 */
declare module '@antv/x6' {
  namespace Node {
    interface Registry {
      [name: FlowNodeType]: Node.Properties;
    }
  }
}

/**
 * 自定义节点工厂
 */
export class FlowNodeFactory {
  /**
   * 创建开始节点
   */
  static createStartNode(id: string): Node {
    return Node.create({
      id,
      shape: 'start',
      x: 100,
      y: 100,
      width: 120,
      height: 50,
      attrs: {
        body: { fill: '#52c41a', stroke: '#389e0d' },
        label: { fill: '#fff', text: '开始' }
      }
    });
  }
  
  /**
   * 创建结束节点
   */
  static createEndNode(id: string): Node {
    return Node.create({
      id,
      shape: 'end',
      x: 100,
      y: 100,
      width: 120,
      height: 50,
      attrs: {
        body: { fill: '#ff4d4f', stroke: '#cf1322' },
        label: { fill: '#fff', text: '结束' }
      }
    });
  }
  
  /**
   * 创建操作节点
   */
  static createOperationNode(id: string, label: string): Node {
    return Node.create({
      id,
      shape: 'operation',
      x: 100,
      y: 100,
      width: 140,
      height: 60,
      attrs: {
        body: { fill: '#1890ff', stroke: '#096dd9' },
        label: { fill: '#fff', text: label }
      }
    });
  }
  
  /**
   * 创建审批节点
   */
  static createApproveNode(id: string, label: string): Node {
    return Node.create({
      id,
      shape: 'approve',
      x: 100,
      y: 100,
      width: 140,
      height: 60,
      attrs: {
        body: { fill: '#faad14', stroke: '#d48806' },
        label: { fill: '#fff', text: label }
      }
    });
  }
  
  /**
   * 创建并行审批节点
   */
  static createParallelApprovalNode(id: string): Node {
    return Node.create({
      id,
      shape: 'parallel-approval',
      x: 100,
      y: 100,
      width: 140,
      height: 60,
      attrs: {
        body: { fill: '#722ed1', stroke: '#531dab' },
        label: { fill: '#fff', text: '并行审批' }
      }
    });
  }
}

/**
 * 自定义边工厂
 */
export class FlowEdgeFactory {
  /**
   * 创建默认边
   */
  static createEdge(sourceId: string, targetId: string, options?: Partial<FlowEdgeProps>): Edge {
    return Edge.create({
      source: sourceId,
      target: targetId,
      attrs: {
        line: { stroke: '#a0a0a0', strokeWidth: 2 }
      },
      ...options
    });
  }
  
  /**
   * 创建条件边
   */
  static createConditionEdge(sourceId: string, targetId: string, condition: string): Edge {
    return Edge.create({
      source: sourceId,
      target: targetId,
      label: condition,
      attrs: {
        line: { stroke: '#faad14', strokeWidth: 2, strokeDasharray: '5 5' }
      },
      edgeType: 'condition',
      condition
    });
  }
}
```

### 图形事件类型安全

```typescript
// src/app/pages/flow-graph/types/graph-events.types.ts

import { EventArgs } from '@antv/x6';

/**
 * 图形事件类型映射
 */
export type GraphEventMap = {
  /** 节点添加事件 */
  'node:added': NodeAddedEvent;
  /** 节点删除事件 */
  'node:removed': NodeRemovedEvent;
  /** 节点移动事件 */
  'node:move': NodeMoveEvent;
  /** 节点调整大小事件 */
  'node:resize': NodeResizeEvent;
  /** 节点点击事件 */
  'node:click': NodeClickEvent;
  /** 节点双击事件 */
  'node:dblclick': NodeDblClickEvent;
  /** 边添加事件 */
  'edge:added': EdgeAddedEvent;
  /** 边删除事件 */
  'edge:removed': EdgeRemovedEvent;
  /** 边连接事件 */
  'edge:connected': EdgeConnectedEvent;
  /** 选择变化事件 */
  'selection:changed': SelectionChangedEvent;
  /** 空白处点击事件 */
  'blank:click': BlankClickEvent;
};

/**
 * 节点添加事件
 */
export interface NodeAddedEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 当前节点 */
  current: Node.Node;
  /** 之前的节点 */
  previous: Node.Node;
  /** 索引 */
  index: number;
}

/**
 * 节点删除事件
 */
export interface NodeRemovedEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 节点索引 */
  index: number;
}

/**
 * 节点移动事件
 */
export interface NodeMoveEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 旧位置 */
  oldPosition: Node.Position;
  /** 新位置 */
  newPosition: Node.Position;
}

/**
 * 节点调整大小事件
 */
export interface NodeResizeEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 旧大小 */
  oldSize: Node.Size;
  /** 新大小 */
  newSize: Node.Size;
}

/**
 * 节点点击事件
 */
export interface NodeClickEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 事件对象 */
  e: MouseEvent;
}

/**
 * 节点双击事件
 */
export interface NodeDblClickEvent {
  /** 节点实例 */
  node: Node.Node;
  /** 事件对象 */
  e: MouseEvent;
}

/**
 * 边添加事件
 */
export interface EdgeAddedEvent {
  /** 边实例 */
  edge: Edge.Edge;
  /** 当前边 */
  current: Edge.Edge;
  /** 之前的边 */
  previous: Edge.Edge;
  /** 索引 */
  index: number;
}

/**
 * 边删除事件
 */
export interface EdgeRemovedEvent {
  /** 边实例 */
  edge: Edge.Edge;
  /** 边索引 */
  index: number;
}

/**
 * 边连接事件
 */
export interface EdgeConnectedEvent {
  /** 边实例 */
  edge: Edge.Edge;
  /** 源节点ID */
  sourceId: string;
  /** 目标节点ID */
  targetId: string;
  /** 源端口 */
  sourcePort?: string;
  /** 目标端口 */
  targetPort?: string;
}

/**
 * 选择变化事件
 */
export interface SelectionChangedEvent {
  /** 新增的单元格 */
  added: Cell[];
  /** 移除的单元格 */
  removed: Cell[];
  /** 选中的单元格 */
  selected: Cell[];
}

/**
 * 空白处点击事件
 */
export interface BlankClickEvent {
  /** 事件对象 */
  e: MouseEvent;
  /** 坐标 */
  x: number;
  y: number;
}

/**
 * 类型安全的事件监听器
 */
export class TypedGraphEventHandler {
  /**
   * 添加事件监听
   * @param graph 图形实例
   * @param eventName 事件名称
   * @param handler 事件处理函数
   */
  static on<K extends keyof GraphEventMap>(
    graph: Graph,
    eventName: K,
    handler: (args: GraphEventMap[K]) => void
  ): void {
    graph.on(eventName, handler as any);
  }
  
  /**
   * 移除事件监听
   * @param graph 图形实例
   * @param eventName 事件名称
   * @param handler 事件处理函数
   */
  static off<K extends keyof GraphEventMap>(
    graph: Graph,
    eventName: K,
    handler: (args: GraphEventMap[K]) => void
  ): void {
    graph.off(eventName, handler as any);
  }
  
  /**
   * 触发事件
   * @param graph 图形实例
   * @param eventName 事件名称
   * @param args 事件参数
   */
  static trigger<K extends keyof GraphEventMap>(
    graph: Graph,
    eventName: K,
    args: GraphEventMap[K]
  ): void {
    graph.trigger(eventName, args as any);
  }
}
```

---

## 业务组件封装规范

### ng-zorro-antd 业务组件封装

```typescript
// src/app/shared/components/app-table/app-table.component.ts

import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { CommonModule } from '@angular/common';
import { PaginationResponse } from '@app/models/base.entity';

/**
 * 表格列配置
 */
export interface AppTableColumn<T> {
  /** 列键名 */
  key: keyof T;
  /** 列标题 */
  title: string;
  /** 列宽度 */
  width?: number;
  /** 是否固定 */
  fixed?: 'left' | 'right' | false;
  /** 对齐方式 */
  align?: 'left' | 'center' | 'right';
  /** 是否可排序 */
  sortable?: boolean;
  /** 是否可筛选 */
  filterable?: boolean;
  /** 自定义渲染函数 */
  render?: (data: T, index: number) => string | TemplateRef<any>;
}

/**
 * 表格操作配置
 */
export interface AppTableAction<T> {
  /** 按钮标签 */
  label: string;
  /** 按钮类型 */
  type?: 'primary' | 'default' | 'dashed' | 'link' | 'text';
  /** 按钮图标 */
  icon?: string;
  /** 是否显示 */
  visible?: (data: T) => boolean;
  /** 是否禁用 */
  disabled?: (data: T) => boolean;
  /** 点击事件 */
  onClick: (data: T, index: number) => void;
  /** 危险操作 */
  danger?: boolean;
}

/**
 * 表格配置
 */
export interface AppTableConfig<T> {
  /** 表格列配置 */
  columns: AppTableColumn<T>[];
  /** 表格操作配置 */
  actions?: AppTableAction<T>[];
  /** 是否显示复选框 */
  showCheckbox?: boolean;
  /** 是否显示序号 */
  showIndex?: boolean;
  /** 是否启用边框 */
  bordered?: boolean;
  /** 表格大小 */
  size?: 'middle' | 'small' | 'default';
  /** 空数据提示 */
  emptyText?: string;
  /** 是否启用分页 */
  showPagination?: boolean;
  /** 每页数量选项 */
  pageSizeOptions?: number[];
}

/**
 * 应用表格组件 - 封装 ng-zorro-antd 表格
 * @template T 实体类型
 */
@Component({
  selector: 'app-table',
  templateUrl: './app-table.component.html',
  styleUrls: ['./app-table.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzButtonModule,
    NzIconModule,
    NzPaginationModule
  ]
})
export class AppTableComponent<T> implements OnChanges {
  
  /** 表格数据 */
  @Input() data: T[] = [];
  
  /** 总记录数 */
  @Input() total = 0;
  
  /** 当前页码 */
  @Input() pageIndex = 1;
  
  /** 每页数量 */
  @Input() pageSize = 10;
  
  /** 加载状态 */
  @Input() loading = false;
  
  /** 表格配置 */
  @Input() config!: AppTableConfig<T>;
  
  /** 选中的行 */
  @Input() selectedRows: T[] = [];
  
  /** 分页变化事件 */
  @Output() pageIndexChange = new EventEmitter<number>();
  
  /** 每页数量变化事件 */
  @Output() pageSizeChange = new EventEmitter<number>();
  
  /** 选中行变化事件 */
  @Output() selectedRowsChange = new EventEmitter<T[]>();
  
  /** 行点击事件 */
  @Output() rowClick = new EventEmitter<{ data: T; index: number }>();
  
  /** 所有选中项 */
  setOfCheckedId = new Set<keyof T>();
  
  /** 是否全部选中 */
  isAllDisplayDataChecked = false;
  
  /** 是否半选 */
  isIndeterminate = false;
  
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedRows']) {
      this.refreshCheckedStatus();
    }
  }
  
  /**
   * 分页查询参数变化
   */
  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.pageSizeChange.emit(pageSize);
    this.pageIndexChange.emit(pageIndex);
  }
  
  /**
   * 更新选中状态
   */
  refreshCheckedStatus(): void {
    this.setOfCheckedId.clear();
    this.selectedRows.forEach(row => {
      // 使用 ID 或唯一标识
      const id = (row as any).id;
      if (id) {
        this.setOfCheckedId.add(id);
      }
    });
    
    this.isAllDisplayDataChecked = this.data.every(item => this.setOfCheckedId.has((item as any).id));
    this.isIndeterminate = !this.isAllDisplayDataChecked && this.selectedRows.length > 0;
  }
  
  /**
   * 全选/取消全选
   */
  onAllChecked(checked: boolean): void {
    this.data.forEach(item => {
      this.updateCheckedSet((item as any).id, checked);
    });
    this.refreshCheckedStatus();
    this.emitSelectedRows();
  }
  
  /**
   * 单行选中
   */
  onItemChecked(id: keyof T, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
    this.emitSelectedRows();
  }
  
  /**
   * 更新选中集合
   */
  private updateCheckedSet(id: keyof T, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }
  
  /**
   * 发送选中行数据
   */
  private emitSelectedRows(): void {
    const selectedRows = this.data.filter(item => this.setOfCheckedId.has((item as any).id));
    this.selectedRowsChange.emit(selectedRows);
  }
  
  /**
   * 获取可见的操作按钮
   */
  getVisibleActions(row: T): AppTableAction<T>[] {
    return (this.config.actions || []).filter(action => {
      return action.visible ? action.visible(row) : true;
    });
  }
  
  /**
   * 获取是否禁用
   */
  getActionDisabled(row: T, action: AppTableAction<T>): boolean {
    return action.disabled ? action.disabled(row) : false;
  }
  
  /**
   * 处理操作点击
   */
  handleActionClick(row: T, index: number, action: AppTableAction<T>): void {
    action.onClick(row, index);
  }
}
```

### 表单组件封装

```typescript
// src/app/shared/components/app-form/app-form.component.ts

import { Component, Input, Output, EventEmitter, ContentChild, TemplateRef } from '@angular/core';
import { NzFormModule } from 'ng-zorro-antd/form';
import { CommonModule } from '@angular/common';

/**
 * 表单项配置
 */
export interface AppFormItem {
  /** 字段键名 */
  key: string;
  /** 字段标题 */
  label: string;
  /** 字段类型 */
  type: 'input' | 'textarea' | 'select' | 'date' | 'number' | 'switch' | 'upload';
  /** 是否必填 */
  required?: boolean;
  /** 占位符 */
  placeholder?: string;
  /** 禁用状态 */
  disabled?: boolean;
  /** 只读状态 */
  readonly?: boolean;
  /** 字段提示 */
  tip?: string;
  /** 默认值 */
  defaultValue?: any;
  /** 选项列表 (用于 select) */
  options?: Array<{ label: string; value: any }>;
  /** 条件显示 */
  visibleIf?: (formData: Record<string, any>) => boolean;
  /** 条件禁用 */
  disabledIf?: (formData: Record<string, any>) => boolean;
}

/**
 * 应用表单组件 - 封装通用表单逻辑
 * @template T 表单数据类型
 */
@Component({
  selector: 'app-form',
  templateUrl: './app-form.component.html',
  styleUrls: ['./app-form.component.css'],
  standalone: true,
  imports: [CommonModule, NzFormModule]
})
export class AppFormComponent<T extends Record<string, any>> {
  
  /** 表单数据 */
  @Input() data!: Partial<T>;
  
  /** 表单项配置 */
  @Input() items!: AppFormItem[];
  
  /** 标签宽度 */
  @Input() labelWidth = '120px';
  
  /** 布局方式 */
  @Input() layout: 'horizontal' | 'vertical' | 'inline' = 'horizontal';
  
  /** 表单大小 */
  @Input() size: 'large' | 'default' | 'small' = 'default';
  
  /** 提交按钮文本 */
  @Input() submitText = '提交';
  
  /** 重置按钮文本 */
  @Input() resetText = '重置';
  
  /** 是否显示取消按钮 */
  @Input() showCancel = false;
  
  /** 取消按钮文本 */
  @Input() cancelText = '取消';
  
  /** 是否禁用提交按钮 */
  @Input() submitDisabled = false;
  
  /** 是否显示提交按钮 */
  @Input() showSubmit = true;
  
  /** 是否显示重置按钮 */
  @Input() showReset = true;
  
  /** 提交事件 */
  @Output() submit = new EventEmitter<T>();
  
  /** 取消事件 */
  @Output() cancel = new EventEmitter<void>();
  
  /** 值变化事件 */
  @Output() valueChange = new EventEmitter<Partial<T>>();
  
  /** 自定义底部模板 */
  @ContentChild('footer') footerTemplate!: TemplateRef<any>;
  
  /** 验证状态映射 */
  validateStatusMap: Map<string, 'success' | 'warning' | 'error' | 'validating'> = new Map();
  
  /** 错误提示映射 */
  errorMsgMap: Map<string, string> = new Map();
  
  /**
   * 获取表单项值
   */
  getItemValue(key: string): any {
    return this.data?.[key];
  }
  
  /**
   * 设置表单项值
   */
  setItemValue(key: string, value: any): void {
    if (this.data) {
      this.data[key] = value;
      this.valueChange.emit({ ...this.data });
    }
  }
  
  /**
   * 获取可见的表单项
   */
  getVisibleItems(): AppFormItem[] {
    return this.items.filter(item => {
      if (!item.visibleIf) return true;
      return item.visibleIf(this.data || {});
    });
  }
  
  /**
   * 获取表单项禁用状态
   */
  getItemDisabled(item: AppFormItem): boolean {
    if (item.disabledIf) {
      return item.disabledIf(this.data || {});
    }
    return item.disabled || false;
  }
  
  /**
   * 提交表单
   */
  handleSubmit(): void {
    if (this.validateForm()) {
      this.submit.emit(this.data as T);
    }
  }
  
  /**
   * 重置表单
   */
  handleReset(): void {
    const resetData: Partial<T> = {};
    this.items.forEach(item => {
      resetData[item.key as keyof T] = item.defaultValue;
    });
    this.data = resetData;
    this.validateStatusMap.clear();
    this.errorMsgMap.clear();
    this.valueChange.emit(resetData);
  }
  
  /**
   * 取消操作
   */
  handleCancel(): void {
    this.cancel.emit();
  }
  
  /**
   * 验证表单
   */
  validateForm(): boolean {
    let isValid = true;
    this.validateStatusMap.clear();
    this.errorMsgMap.clear();
    
    const visibleItems = this.getVisibleItems();
    
    for (const item of visibleItems) {
      if (item.required && !this.getItemValue(item.key)) {
        this.validateStatusMap.set(item.key, 'error');
        this.errorMsgMap.set(item.key, `${item.label}不能为空`);
        isValid = false;
      }
    }
    
    return isValid;
  }
  
  /**
   * 验证单个字段
   */
  validateItem(key: string): boolean {
    const item = this.items.find(i => i.key === key);
    if (!item) return true;
    
    if (item.required && !this.getItemValue(key)) {
      this.validateStatusMap.set(key, 'error');
      this.errorMsgMap.set(key, `${item.label}不能为空`);
      return false;
    }
    
    this.validateStatusMap.set(key, 'success');
    this.errorMsgMap.delete(key);
    return true;
  }
}
```

---

## RxJS 类型安全

### 类型化服务层

```typescript
// src/app/core/base-http.service.ts

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiResponse, PaginationResponse, ListQueryParams } from '../models/base.entity';

/**
 * HTTP 请求配置
 */
export interface HttpRequestConfig {
  /** 请求头 */
  headers?: Record<string, string>;
  /** 请求参数 */
  params?: Record<string, any>;
  /** 报告进度 */
  reportProgress?: boolean;
  /** 响应类型 */
  responseType?: 'arraybuffer' | 'blob' | 'json' | 'text';
}

/**
 * 基础 HTTP 服务 - 提供类型安全的 HTTP 请求方法
 */
@Injectable({ providedIn: 'root' })
export class BaseHttpService {
  
  constructor(private http: HttpClient) {}
  
  /**
   * GET 请求
   * @template T 响应数据类型
   * @param url 请求地址
   * @param config 请求配置
   * @returns Observable<T>
   */
  get<T>(url: string, config?: HttpRequestConfig): Observable<T> {
    return this.http.get<ApiResponse<T>>(url, this.buildConfig(config)).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * GET 请求 (分页)
   * @template T 响应数据类型
   * @param url 请求地址
   * @param params 查询参数
   * @returns Observable<PaginationResponse<T>>
   */
  getPage<T>(url: string, params: ListQueryParams): Observable<PaginationResponse<T>> {
    return this.http.get<ApiResponse<PaginationResponse<T>>>(url, {
      params: this.buildHttpParams(params)
    }).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * POST 请求
   * @template T 请求数据类型
   * @template R 响应数据类型
   * @param url 请求地址
   * @param data 请求数据
   * @param config 请求配置
   * @returns Observable<R>
   */
  post<T, R = T>(url: string, data: T, config?: HttpRequestConfig): Observable<R> {
    return this.http.post<ApiResponse<R>>(url, data, this.buildConfig(config)).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * PUT 请求
   * @template T 请求数据类型
   * @template R 响应数据类型
   * @param url 请求地址
   * @param data 请求数据
   * @param config 请求配置
   * @returns Observable<R>
   */
  put<T, R = T>(url: string, data: T, config?: HttpRequestConfig): Observable<R> {
    return this.http.put<ApiResponse<R>>(url, data, this.buildConfig(config)).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * DELETE 请求
   * @template R 响应数据类型
   * @param url 请求地址
   * @param config 请求配置
   * @returns Observable<R>
   */
  delete<R = void>(url: string, config?: HttpRequestConfig): Observable<R> {
    return this.http.delete<ApiResponse<R>>(url, this.buildConfig(config)).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * PATCH 请求
   * @template T 请求数据类型
   * @template R 响应数据类型
   * @param url 请求地址
   * @param data 请求数据
   * @param config 请求配置
   * @returns Observable<R>
   */
  patch<T, R = T>(url: string, data: T, config?: HttpRequestConfig): Observable<R> {
    return this.http.patch<ApiResponse<R>>(url, data, this.buildConfig(config)).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * 上传文件
   * @param url 上传地址
   * @param file 文件对象
   * @param config 请求配置
   * @returns Observable<R>
   */
  upload<R = any>(url: string, file: File, config?: HttpRequestConfig): Observable<R> {
    const formData = new FormData();
    formData.append('file', file);
    
    return this.http.post<ApiResponse<R>>(url, formData, {
      ...this.buildConfig(config),
      reportProgress: true,
      observe: 'events'
    }).pipe(
      map(response => response.data!),
      catchError(error => this.handleError(error))
    );
  }
  
  /**
   * 下载文件
   * @param url 下载地址
   * @param config 请求配置
   * @returns Observable<Blob>
   */
  download(url: string, config?: HttpRequestConfig): Observable<Blob> {
    return this.http.get(url, {
      ...this.buildConfig(config),
      responseType: 'blob'
    });
  }
  
  /**
   * 构建请求配置
   */
  private buildConfig(config?: HttpRequestConfig): any {
    return {
      headers: config?.headers,
      params: config?.params ? this.buildHttpParams(config.params) : undefined,
      reportProgress: config?.reportProgress,
      responseType: config?.responseType || 'json'
    };
  }
  
  /**
   * 构建 HttpParams
   */
  private buildHttpParams(params: Record<string, any>): HttpParams {
    let httpParams = new HttpParams();
    
    Object.keys(params).forEach(key => {
      const value = params[key];
      if (value !== null && value !== undefined) {
        if (Array.isArray(value)) {
          value.forEach(v => httpParams = httpParams.append(key, String(v)));
        } else {
          httpParams = httpParams.set(key, String(value));
        }
      }
    });
    
    return httpParams;
  }
  
  /**
   * 错误处理
   */
  private handleError(error: any): Observable<never> {
    console.error('HTTP Error:', error);
    return throwError(() => error);
  }
}
```

### 具体业务服务示例

```typescript
// src/app/services/approval-process.service.ts

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { BaseHttpService } from '@core/base-http.service';
import { BaseEntity, PaginationResponse, ListQueryParams } from '../models/base.entity';

/**
 * 审批流程实体
 */
export interface ApprovalProcess extends BaseEntity {
  /** 流程名称 */
  name: string;
  /** 流程描述 */
  description: string;
  /** 审批类型 */
  approvalType: 'single' | 'multi' | 'parallel';
  /** 审批步骤数 */
  stepCount: number;
  /** 是否启用 */
  enabled: boolean;
  /** 流程版本 */
  version: number;
}

/**
 * 审批流程查询参数
 */
export interface ApprovalProcessQueryParams extends ListQueryParams {
  /** 流程名称 */
  name?: string;
  /** 审批类型 */
  approvalType?: string;
  /** 是否启用 */
  enabled?: boolean;
}

/**
 * 审批流程统计信息
 */
export interface ApprovalProcessStatistics {
  /** 总流程数 */
  total: number;
  /** 启用流程数 */
  enabled: number;
  /** 禁用流程数 */
  disabled: number;
  /** 今日创建数 */
  todayCreated: number;
}

/**
 * 审批流程服务
 */
@Injectable({ providedIn: 'root' })
export class ApprovalProcessService {
  
  private readonly baseUrl = '/api/approval-process';
  
  constructor(private http: BaseHttpService) {}
  
  /**
   * 获取审批流程列表
   */
  getList(params: ApprovalProcessQueryParams): Observable<PaginationResponse<ApprovalProcess>> {
    return this.http.getPage<ApprovalProcess>(`${this.baseUrl}/list`, params);
  }
  
  /**
   * 获取审批流程详情
   */
  getDetail(id: string): Observable<ApprovalProcess> {
    return this.http.get<ApprovalProcess>(`${this.baseUrl}/${id}`);
  }
  
  /**
   * 创建审批流程
   */
  create(data: Omit<ApprovalProcess, keyof BaseEntity>): Observable<ApprovalProcess> {
    return this.http.post<typeof data, ApprovalProcess>(`${this.baseUrl}`, data);
  }
  
  /**
   * 更新审批流程
   */
  update(id: string, data: Partial<ApprovalProcess>): Observable<ApprovalProcess> {
    return this.http.put<typeof data, ApprovalProcess>(`${this.baseUrl}/${id}`, data);
  }
  
  /**
   * 删除审批流程
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  
  /**
   * 批量删除审批流程
   */
  batchDelete(ids: string[]): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/batch`, { params: { ids } });
  }
  
  /**
   * 启用审批流程
   */
  enable(id: string): Observable<ApprovalProcess> {
    return this.http.patch<void, ApprovalProcess>(`${this.baseUrl}/${id}/enable`, {});
  }
  
  /**
   * 禁用审批流程
   */
  disable(id: string): Observable<ApprovalProcess> {
    return this.http.patch<void, ApprovalProcess>(`${this.baseUrl}/${id}/disable`, {});
  }
  
  /**
   * 获取统计信息
   */
  getStatistics(): Observable<ApprovalProcessStatistics> {
    return this.http.get<ApprovalProcessStatistics>(`${this.baseUrl}/statistics`);
  }
}
```

---

## 依赖注入类型策略

### 混合策略实现

```typescript
// src/app/core/app.config.ts

import { ApplicationConfig, importProvidersFrom, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideNzIcons } from 'ng-zorro-antd/icon';
import * as icons from '@ant-design/icons-angular/icons';
import { DelonModule } from '@delon/abc/delon.module';
import { provideAnimations } from '@angular/platform-browser/animations';

/**
 * 应用配置常量
 */
export const APP_CONFIG = {
  /** API 基础地址 */
  apiBaseUrl: 'http://localhost:3000/api',
  /** 应用名称 */
  appName: 'ApFlow.UI',
  /** 版本号 */
  version: '1.0.0',
  /** 是否启用调试模式 */
  debug: isDevMode()
};

/**
 * 应用配置 Token - 使用 InjectionToken 提供类型安全
 */
import { InjectionToken } from '@angular/core';

export const APP_CONFIG_TOKEN = new InjectionToken<typeof APP_CONFIG>('app-config');

/**
 * 应用程序配置
 */
export const appConfig: ApplicationConfig = {
  providers: [
    // 路由配置
    provideRouter(routes),
    
    // HTTP 配置
    provideHttpClient(
      withInterceptors([authInterceptor, errorInterceptor])
    ),
    
    // 动画
    provideAnimations(),
    
    // ng-zorro-antd 图标
    provideNzIcons(...Object.values(icons)),
    
    // 应用配置
    { provide: APP_CONFIG_TOKEN, useValue: APP_CONFIG },
    
    // 业务服务 (简单场景 - 无类型约束)
    ApprovalProcessService,
    EditorService,
    UserService,
    
    // 复杂场景 - 使用 InjectionToken + 泛型类型约束
    { provide: EDITOR_CONFIG_TOKEN, useValue: DEFAULT_EDITOR_CONFIG },
    { provide: STORAGE_SERVICE_TOKEN, useClass: LocalStorageService },
    
    // Delon 模块
    importProvidersFrom(DelonModule)
  ]
};

/**
 * 编辑器配置
 */
export interface EditorConfig {
  /** 是否启用网格 */
  enableGrid: boolean;
  /** 网格大小 */
  gridSize: number;
  /** 是否启用对齐线 */
  enableSnapline: boolean;
  /** 背景颜色 */
  backgroundColor: string;
  /** 缩放范围 */
  zoomRange: [number, number];
}

/**
 * 默认编辑器配置
 */
export const DEFAULT_EDITOR_CONFIG: EditorConfig = {
  enableGrid: true,
  gridSize: 10,
  enableSnapline: true,
  backgroundColor: '#f5f5f5',
  zoomRange: [0.1, 5]
};

/**
 * 编辑器配置 Token
 */
export const EDITOR_CONFIG_TOKEN = new InjectionToken<EditorConfig>('editor-config');

/**
 * 存储服务接口
 */
export interface IStorageService<T = any> {
  /** 设置数据 */
  set(key: string, value: T): void;
  /** 获取数据 */
  get(key: string): T | null;
  /** 删除数据 */
  remove(key: string): void;
  /** 清空数据 */
  clear(): void;
}

/**
 * 本地存储服务实现
 */
@Injectable({ providedIn: 'root' })
export class LocalStorageService implements IStorageService {
  set<T>(key: string, value: T): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
  
  get<T>(key: string): T | null {
    const value = localStorage.getItem(key);
    return value ? JSON.parse(value) : null;
  }
  
  remove(key: string): void {
    localStorage.removeItem(key);
  }
  
  clear(): void {
    localStorage.clear();
  }
}

/**
 * 存储服务 Token - 泛型类型约束
 */
export const STORAGE_SERVICE_TOKEN = new InjectionToken<IStorageService>('storage-service');
```

### 使用 InjectionToken 的服务

```typescript
// src/app/services/editor.service.ts

import { Injectable, Inject, inject } from '@angular/core';
import { Graph } from '@antv/x6';
import { FlowGraph } from '../pages/flow-graph/models/flow-graph';
import { EDITOR_CONFIG_TOKEN, EditorConfig } from '@core/app.config';
import { APP_CONFIG_TOKEN } from '@core/app.config';

@Injectable({ providedIn: 'root' })
export class EditorService {
  
  /** 图形实例 */
  private graph: Graph | null = null;
  
  /** 流程图数据 */
  private flowGraph: FlowGraph | null = null;
  
  /** 编辑器配置 - 使用 InjectionToken */
  private config: EditorConfig;
  
  /** 应用配置 - 使用 InjectionToken */
  private appConfig: typeof APP_CONFIG;
  
  constructor(
    @Inject(EDITOR_CONFIG_TOKEN) config: EditorConfig,
    @Inject(APP_CONFIG_TOKEN) appConfig: typeof APP_CONFIG
  ) {
    this.config = config;
    this.appConfig = appConfig;
    
    console.log('Editor config:', this.config);
    console.log('App config:', this.appConfig);
  }
  
  /**
   * 设置图形实例
   */
  setGraph(graph: Graph): void {
    this.graph = graph;
  }
  
  /**
   * 获取图形实例
   */
  getGraph(): Graph | null {
    return this.graph;
  }
  
  /**
   * 设置流程图数据
   */
  setFlowGraph(flowGraph: FlowGraph): void {
    this.flowGraph = flowGraph;
  }
  
  /**
   * 获取流程图数据
   */
  getFlowGraph(): FlowGraph | null {
    return this.flowGraph;
  }
  
  /**
   * 渲染流程图
   */
  renderGraph(): void {
    if (!this.graph || !this.flowGraph) return;
    
    // 清空图形
    this.graph.clearCells();
    
    // 渲染节点
    this.flowGraph.nodes.forEach(node => {
      this.graph?.addNode(node.toX6Node());
    });
    
    // 渲染边
    this.flowGraph.edges.forEach(edge => {
      this.graph?.addEdge(edge.toX6Edge());
    });
    
    // 应用布局
    this.applyLayout();
  }
  
  /**
   * 应用布局
   */
  private applyLayout(): void {
    if (!this.graph) return;
    
    const { DagreLayout } = require('@antv/layout');
    
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      rankdir: 'LR',
      align: 'UL',
      nodesep: 50,
      ranksep: 50,
      controlPoints: true
    });
    
    const model = dagreLayout.layout(this.graph.toJSON());
    this.graph.fromJSON(model);
  }
  
  /**
   * 导出流程图
   */
  exportGraph(): string {
    if (!this.flowGraph) return '';
    return JSON.stringify(this.flowGraph);
  }
  
  /**
   * 导入流程图
   */
  importGraph(data: string): void {
    try {
      const graphData = JSON.parse(data);
      this.flowGraph = graphData as FlowGraph;
      this.renderGraph();
    } catch (error) {
      console.error('导入流程图失败:', error);
    }
  }
}
```

---

## 测试类型规范

### 类型断言测试

```typescript
// src/app/services/editor.service.spec.ts

import { TestBed } from '@angular/core/testing';
import { EditorService } from './editor.service';
import { EDITOR_CONFIG_TOKEN, DEFAULT_EDITOR_CONFIG } from '@core/app.config';
import { Graph } from '@antv/x6';
import { FlowGraph } from '../pages/flow-graph/models/flow-graph';

describe('EditorService', () => {
  let service: EditorService;
  let graph: Graph;
  let flowGraph: FlowGraph;
  
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        EditorService,
        { provide: EDITOR_CONFIG_TOKEN, useValue: DEFAULT_EDITOR_CONFIG },
        // 提供模拟的应用配置
        {
          provide: 'app-config',
          useValue: {
            apiBaseUrl: 'http://localhost:3000/api',
            appName: 'ApFlow.UI',
            version: '1.0.0',
            debug: true
          }
        }
      ]
    });
    
    service = TestBed.inject(EditorService);
    graph = new Graph({
      container: document.createElement('div'),
      width: 800,
      height: 600
    });
    flowGraph = FlowGraph.new();
  });
  
  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  
  it('should set and get graph', () => {
    service.setGraph(graph);
    
    const retrievedGraph = service.getGraph();
    expect(retrievedGraph).toBe(graph);
    expect(retrievedGraph).toBeTruthy();
  });
  
  it('should set and get flow graph', () => {
    service.setFlowGraph(flowGraph);
    
    const retrievedFlowGraph = service.getFlowGraph();
    expect(retrievedFlowGraph).toBe(flowGraph);
    expect(retrievedFlowGraph).toBeTruthy();
  });
  
  it('should export and import graph correctly', () => {
    service.setFlowGraph(flowGraph);
    
    const exportedData = service.exportGraph();
    expect(exportedData).toBeTruthy();
    expect(typeof exportedData).toBe('string');
    
    service.setFlowGraph(FlowGraph.new()); // 重置
    service.importGraph(exportedData);
    
    const importedFlowGraph = service.getFlowGraph();
    expect(importedFlowGraph).toBeTruthy();
    expect(importedFlowGraph?.nodes).toEqual(flowGraph.nodes);
    expect(importedFlowGraph?.edges).toEqual(flowGraph.edges);
  });
  
  it('should handle invalid import data', () => {
    const consoleSpy = spyOn(console, 'error');
    
    service.importGraph('invalid json');
    
    expect(consoleSpy).toHaveBeenCalled();
    expect(service.getFlowGraph()).toBeNull();
  });
  
  /**
   * 类型断言测试 - 确保测试数据符合生产类型
   */
  it('should maintain type safety for graph operations', () => {
    // 类型断言 - 确保 graph 是 Graph 类型
    expect(graph).toBeInstanceOf(Graph);
    
    // 类型断言 - 确保 flowGraph 是 FlowGraph 类型
    expect(flowGraph).toBeInstanceOf(FlowGraph);
    
    // 类型断言 - 确保 flowGraph.nodes 是 FlowNode 数组
    expect(Array.isArray(flowGraph.nodes)).toBeTrue();
    
    // 类型断言 - 确保 flowGraph.edges 是 FlowEdge 数组
    expect(Array.isArray(flowGraph.edges)).toBeTrue();
    
    // 类型断言 - 确保 nodes 中的元素有必需的属性
    flowGraph.nodes.forEach(node => {
      expect(node).toHaveProperty('id');
      expect(node).toHaveProperty('shape');
      expect(typeof node.id).toBe('string');
    });
  });
});
```

### 组件测试

```typescript
// src/app/pages/flow-graph/components/editor/editor.component.spec.ts

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditorComponent, EditorComponentConfig, EditorEvents } from './editor.component';
import { EditorService } from '../../services/editor.service';
import { NgZone } from '@angular/core';

describe('EditorComponent', () => {
  let component: EditorComponent;
  let fixture: ComponentFixture<EditorComponent>;
  let editorServiceSpy: jasmine.SpyObj<EditorService>;
  
  const mockConfig: EditorComponentConfig = {
    enableGrid: true,
    enablePanning: true,
    enableMousewheel: true,
    enableSnapline: true,
    backgroundColor: '#ffffff'
  };
  
  beforeEach(async () => {
    const spy = jasmine.createSpyObj('EditorService', ['setGraph', 'setFlowGraph', 'renderGraph']);
    
    await TestBed.configureTestingModule({
      declarations: [EditorComponent],
      providers: [
        { provide: EditorService, useValue: spy }
      ]
    }).compileComponents();
    
    editorServiceSpy = TestBed.inject(EditorService) as jasmine.SpyObj<EditorService>;
    fixture = TestBed.createComponent(EditorComponent);
    component = fixture.componentInstance;
    component.config = mockConfig;
  });
  
  it('should create', () => {
    expect(component).toBeTruthy();
  });
  
  it('should initialize with default config', () => {
    expect(component.config).toEqual(mockConfig);
  });
  
  it('should emit nodeAdded event when node is added', (done: DoneFn) => {
    // 类型断言 - 事件参数类型正确
    component.nodeAdded.subscribe((event: EditorEvents['nodeAdded']) => {
      expect(event).toHaveProperty('nodeId');
      expect(event).toHaveProperty('nodeType');
      expect(typeof event.nodeId).toBe('string');
      expect(typeof event.nodeType).toBe('string');
      done();
    });
    
    // 模拟节点添加事件
    // (需要实际的图形事件触发)
  });
  
  it('should emit selectionChanged event', (done: DoneFn) => {
    component.selectionChanged.subscribe((event: EditorEvents['selectionChanged']) => {
      expect(event).toHaveProperty('selectedIds');
      expect(Array.isArray(event.selectedIds)).toBeTrue();
      done();
    });
    
    // 模拟选择变化事件
  });
});
```

---

## 实施路线图

### 阶段一: 基础设施建设 (Week 1-2)

**目标**: 建立类型安全的基础设施

**任务**:
1. ✅ 完善 `tsconfig.json` 配置
2. ✅ 创建实用类型定义文件 (`src/types/util.types.ts`)
3. ✅ 创建实体基类定义 (`src/app/models/base.entity.ts`)
4. ✅ 创建基础 HTTP 服务 (`src/app/core/base-http.service.ts`)
5. ✅ 设置路径映射

**验收标准**:
- TypeScript 编译无错误
- 类型检查通过
- 基础类型定义完整

---

### 阶段二: 动态表单类型系统 (Week 3-4)

**目标**: 建立类型安全的动态表单系统

**任务**:
1. ✅ 创建 `@delon/form` 类型扩展 (`src/types/delon-form.types.ts`)
2. ✅ 实现动态表单基类 (`BaseDynamicFormComponent`)
3. ✅ 为现有表单组件添加类型定义
4. ✅ 编写表单类型测试用例

**验收标准**:
- 表单组件具有完整的类型推断
- 表单验证类型安全
- 测试覆盖率 > 80%

---

### 阶段三: AntV X6 类型集成 (Week 5-6)

**目标**: 建立类型安全的图形编辑系统

**任务**:
1. ✅ 定义自定义节点类型 (`src/app/pages/flow-graph/types/x6-custom.types.ts`)
2. ✅ 定义图形事件类型 (`src/app/pages/flow-graph/types/graph-events.types.ts`)
3. ✅ 创建节点和边工厂类
4. ✅ 实现类型安全的事件处理
5. ✅ 优化 `FlowGraph` 类型定义

**验收标准**:
- 图形编辑器类型安全
- 自定义节点和边类型完整
- 事件处理类型正确

---

### 阶段四: 业务组件封装 (Week 7-8)

**目标**: 封装类型安全的业务组件

**任务**:
1. ✅ 封装表格组件 (`AppTableComponent`)
2. ✅ 封装表单组件 (`AppFormComponent`)
3. ✅ 封装其他常用组件 (按钮、卡片、模态框等)
4. ✅ 编写组件文档和使用示例

**验收标准**:
- 业务组件类型完整
- 组件复用性强
- 文档完善

---

### 阶段五: 依赖注入优化 (Week 9)

**目标**: 优化依赖注入类型策略

**任务**:
1. ✅ 创建应用配置 Token (`APP_CONFIG_TOKEN`)
2. ✅ 创建编辑器配置 Token (`EDITOR_CONFIG_TOKEN`)
3. ✅ 创建存储服务 Token (`STORAGE_SERVICE_TOKEN`)
4. ✅ 优化现有服务的依赖注入

**验收标准**:
- 依赖注入类型安全
- 配置管理清晰
- 服务间解耦

---

### 阶段六: RxJS 类型安全 (Week 10)

**目标**: 确保 RxJS 管道类型安全

**任务**:
1. ✅ 优化基础 HTTP 服务的类型定义
2. ✅ 为业务服务添加类型化方法
3. ✅ 编写 RxJS 管道类型测试
4. ✅ 优化错误处理类型

**验收标准**:
- RxJS 管道类型推断正确
- 无 `any` 类型污染
- 错误处理类型安全

---

### 阶段七: 测试与文档 (Week 11-12)

**目标**: 完善测试和文档

**任务**:
1. ✅ 编写单元测试 (覆盖率 > 80%)
2. ✅ 编写集成测试
3. ✅ 完善 TypeScript 类型注释 (TSDoc)
4. ✅ 编写开发指南
5. ✅ 编写类型规范文档

**验收标准**:
- 测试覆盖率 > 80%
- 文档完善
- 代码注释清晰

---

## 附录

### A. 代码示例

#### 实用类型使用示例

```typescript
import { Nullable, DeepReadonly, PartialNullable, ArrayElement } from '@types/util.types';

interface User {
  id: string;
  name: string;
  email: string;
  age: number;
  address: {
    city: string;
    street: string;
  };
}

// 可空类型
const user: Nullable<User> = null;

// 深度只读
const readonlyUser: DeepReadonly<User> = {
  id: '1',
  name: 'John',
  email: 'john@example.com',
  age: 30,
  address: {
    city: 'New York',
    street: 'Main St'
  }
};

// 部分可空
const partialUser: PartialNullable<User> = {
  id: '1',
  name: null,
  email: 'john@example.com'
};

// 数组元素类型
type UserArray = User[];
type UserType = ArrayElement<UserArray>; // User
```

### B. 最佳实践

#### 1. 类型命名约定

- **接口**: 使用 PascalCase, 以 `I` 开头 (可选)
  ```typescript
  interface User { }
  interface IUser { }
  ```

- **类型别名**: 使用 PascalCase, 以 `T` 开头 (可选)
  ```typescript
  type User = { };
  type TUser = { };
  ```

- **枚举**: 使用 PascalCase
  ```typescript
  enum UserRole { }
  ```

- **泛型参数**: 使用单字母大写 (`T`, `U`, `V`, `K`)
  ```typescript
  function map<T, U>(array: T[], fn: (item: T) => U): U[] { }
  ```

#### 2. 避免使用 `any`

**不推荐**:
```typescript
function processData(data: any): any {
  return data.value;
}
```

**推荐**:
```typescript
interface Data {
  value: string;
}

function processData<T extends Data>(data: T): string {
  return data.value;
}
```

#### 3. 使用泛型约束

```typescript
interface Lengthwise {
  length: number;
}

function logLength<T extends Lengthwise>(arg: T): number {
  return arg.length;
}
```

#### 4. 类型守卫

```typescript
function isString(value: unknown): value is string {
  return typeof value === 'string';
}

function processValue(value: unknown): string {
  if (isString(value)) {
    return value.toUpperCase();
  }
  return '';
}
```

#### 5. 使用 `unknown` 代替 `any`

```typescript
// 不推荐
function parseJson(json: any): any {
  return JSON.parse(json);
}

// 推荐
function parseJson(json: string): unknown {
  return JSON.parse(json);
}
```

### C. 参考资源

- [TypeScript 官方文档](https://www.typescriptlang.org/docs/)
- [Angular 类型安全最佳实践](https://angular.dev/guide/typescript-configuration)
- [AntV X6 类型定义](https://x6.antv.antgroup.com/docs/api/graph)
- [ng-zorro-antd 类型定义](https://ng.ant.design/docs/getting-started/en)
- [@delon/form 文档](https://ng-alain.com/form/getting-started)

---

**文档版本**: 1.0.0  
**最后更新**: 2026-03-03  
**维护者**: 开发团队
