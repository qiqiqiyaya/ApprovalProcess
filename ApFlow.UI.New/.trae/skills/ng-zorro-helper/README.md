# ng-zorro 编程助手技能

## 技能概述

ng-zorro 编程助手是一个专门帮助开发者使用 ng-zorro (Ant Design for Angular) 组件库进行开发的 AI 技能。

## 目录结构

```
ng-zorro-helper/
├── SKILL.md                    # 主技能文件,包含技能说明和使用指南
├── README.md                   # 本文件,说明文档
└── references/                 # 参考文档目录
    ├── llms.txt                # 导航文件,包含所有文档和组件的链接
    ├── llms-full.txt           # 完整的英文组件文档
    └── llms-full-cn.txt        # 完整的中文组件文档
```

## 技能功能

### 1. 组件库知识
- 77+ 个组件的详细 API 文档
- 组件使用示例和最佳实践
- 中英文双语文档支持

### 2. 核心能力
- 项目初始化和配置指导
- 组件使用和集成
- 主题定制和样式配置
- 国际化(i18n)支持
- 性能优化建议

### 3. 组件覆盖
- **通用组件**: Button, Icon, Typography
- **布局组件**: Grid, Layout, Space, Splitter
- **导航组件**: Affix, Breadcrumb, Menu, Pagination
- **数据录入**: Form, Input, Select, DatePicker
- **数据展示**: Table, List, Card, Tree
- **反馈组件**: Modal, Message, Notification

## 使用方式

该技能可以被 skill_load 工具加载使用:

```typescript
// 在需要 ng-zorro 开发帮助时,技能会自动加载
// 或者手动指定技能名称加载
skill_load("ng-zorro-helper")
```

## 适用场景

1. **企业级中后台产品开发** - ng-zorro 专为研发企业级中后台产品设计
2. **Angular 项目 UI 组件集成** - 需要在 Angular 项目中使用 Ant Design 设计语言
3. **表单开发** - 复杂表单场景,包括验证、动态表单等
4. **数据展示** - 表格、列表、树形控件等数据展示需求
5. **用户交互反馈** - 消息提示、通知、对话框等交互场景

## 技术栈

- **框架**: Angular (^21.0.0)
- **组件库**: ng-zorro-antd (Ant Design for Angular)
- **语言**: TypeScript
- **样式**: Less / CSS

## 快速开始

### 安装 ng-zorro-antd

```bash
# 创建新项目
ng new my-project

# 进入项目目录
cd my-project

# 添加 ng-zorro-antd
ng add ng-zorro-antd
```

### 基础使用示例

```typescript
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';

@Component({
  selector: 'app-example',
  standalone: true,
  imports: [NzButtonModule],
  template: `
    <button nz-button nzType="primary">Primary Button</button>
  `
})
export class ExampleComponent {}
```

## 参考资源

- 官方文档: https://ng.ant.design
- GitHub: https://github.com/NG-ZORRO/ng-zorro-antd
- Angular 官方文档: https://angular.dev

## 版本信息

- 技能版本: 1.0.0
- 支持的 Angular 版本: ^21.0.0
- ng-zorro-antd 版本: 与 Angular 主版本保持一致

## 更新日志

### v1.0.0 (2025-04-07)
- 初始版本发布
- 包含完整的 ng-zorro 组件库文档
- 支持中英文双语文档
- 提供 77+ 个组件的使用指南

## 贡献

本技能由 AI 助手自动生成,基于 ng-zorro-antd 官方文档。

## 许可证

本技能遵循 MIT 许可证。
