---
name: ng-zorro-helper
description: ng-zorro 编程助手 - 帮助开发者使用 ng-zorro (Ant Design for Angular) 组件库进行开发
license: MIT
compatibility: 
  angular: "21.x"
  ng-zorro-antd: "21.x"
metadata:
  author: youli
  version: "1.0"
---

# ng-zorro 编程助手

## 技能描述

ng-zorro 编程助手是一个专门帮助开发者使用 ng-zorro (Ant Design for Angular) 组件库进行开发的 AI 技能。该技能提供了完整的 ng-zorro 组件库文档、最佳实践和代码示例,能够帮助开发者快速理解和应用 ng-zorro 的各种组件。

## 技能信息

- **技能名称**: ng-zorro 编程助手
- **版本**: 1.0.0
- **支持框架**: Angular (目前支持 Angular ^21.0.0)
- **组件库**: ng-zorro-antd (Ant Design for Angular)
- **组件数量**: 70+ 高质量组件

## 适用场景

### 主要场景
1. **企业级中后台产品开发** - ng-zorro 专为研发企业级中后台产品设计
2. **Angular 项目 UI 组件集成** - 需要在 Angular 项目中使用 Ant Design 设计语言
3. **表单开发** - 复杂表单场景,包括验证、动态表单等
4. **数据展示** - 表格、列表、树形控件等数据展示需求
5. **用户交互反馈** - 消息提示、通知、对话框等交互场景

### 具体任务类型
- 创建各种 UI 组件(Button、Input、Table、Form 等)
- 配置全局主题和样式定制
- 实现国际化(i18n)功能
- 集成图标系统
- 处理表单验证和提交
- 实现数据表格和分页
- 构建导航菜单和布局
- 实现文件上传功能
- 创建对话框和抽屉
- 实现通知和消息提示

## 核心能力

### 1. 组件库知识
该技能拥有完整的 ng-zorro 组件库文档,包括:
- **77+ 个组件**的详细 API 文档
- 每个组件的使用示例
- 组件属性、事件、方法的完整说明
- 中英文双语文档支持

### 2. 主要组件类别

#### 通用组件
- Button (按钮)
- Icon (图标)
- Typography (排版)

#### 布局组件
- Grid (栅格)
- Layout (布局)
- Space (间距)
- Splitter (分隔面板)

#### 导航组件
- Affix (固钉)
- Breadcrumb (面包屑)
- Dropdown (下拉菜单)
- Menu (导航菜单)
- Pagination (分页)
- Steps (步骤条)

#### 数据录入组件
- AutoComplete (自动完成)
- Cascader (级联选择)
- Checkbox (多选框)
- ColorPicker (颜色选择器)
- DatePicker (日期选择框)
- Form (表单)
- Input (输入框)
- InputNumber (数字输入框)
- Mention (提及)
- Radio (单选框)
- Rate (评分)
- Select (选择器)
- Slider (滑动输入条)
- Switch (开关)
- TimePicker (时间选择框)
- Transfer (穿梭框)
- TreeSelect (树选择)
- Upload (上传)

#### 数据展示组件
- Avatar (头像)
- Badge (徽标数)
- Calendar (日历)
- Card (卡片)
- Carousel (走马灯)
- Collapse (折叠面板)
- Descriptions (描述列表)
- Empty (空状态)
- List (列表)
- Popover (气泡卡片)
- QRCode (二维码)
- Segmented (分段控制器)
- Statistic (统计)
- Table (表格)
- Tabs (标签页)
- Tag (标签)
- Timeline (时间轴)
- Tooltip (文字提示)
- Tree (树形控件)
- TreeView (树视图)

#### 反馈组件
- Alert (警告提示)
- Drawer (抽屉)
- Message (全局提示)
- Modal (对话框)
- Notification (通知提醒框)
- Popconfirm (气泡确认框)
- Progress (进度条)
- Result (结果)
- Skeleton (骨架屏)
- Spin (加载中)

### 3. 开发指导能力

#### 项目初始化
提供完整的项目初始化指导,包括:
- Angular CLI 项目创建
- ng-zorro-antd 安装配置
- 样式引入方式
- 模块导入配置

#### 主题定制
支持多种主题定制方式:
- Less 变量定制
- CSS Variable 动态主题
- 预定义主题(默认、暗黑、紧凑、阿里云)

#### 全局配置
组件级全局配置指导:
- 全局配置项设置
- 运行时动态修改
- 局部配置覆盖

#### 国际化
完整的国际化支持:
- 60+ 种语言支持
- 语言包配置
- 动态语言切换

#### 动画控制
动画效果配置:
- 全局动画开关
- 组件级动画控制
- 波浪效果配置

## 使用方式

### 自动触发
当检测到以下关键词或场景时,该技能会自动被加载:
- "ng-zorro"、"ng-zorro-antd"、"Ant Design Angular"
- 组件名称(如 "nz-button"、"nz-table"、"nz-form" 等)
- Angular UI 开发相关问题
- 中后台界面开发需求

### 使用建议
为了让该技能更好地帮助您,建议:
1. **明确组件需求** - 说明需要使用的具体组件名称
2. **提供使用场景** - 描述具体的使用场景和需求
3. **指定语言偏好** - 说明需要中文还是英文文档
4. **提供代码上下文** - 如果是修改现有代码,请提供相关代码片段

## 快速开始示例

### 1. 安装和配置

```bash
# 创建新项目
ng new my-project

# 进入项目目录
cd my-project

# 添加 ng-zorro-antd
ng add ng-zorro-antd
```

### 2. 基础组件使用

#### 按钮组件
```typescript
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';

@Component({
  selector: 'app-example',
  standalone: true,
  imports: [NzButtonModule],
  template: `
    <button nz-button nzType="primary">Primary Button</button>
    <button nz-button nzType="default">Default Button</button>
    <button nz-button nzType="dashed">Dashed Button</button>
    <button nz-button nzType="link">Link Button</button>
  `
})
export class ExampleComponent {}
```

#### 表格组件
```typescript
import { Component } from '@angular/core';
import { NzTableModule } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-table-example',
  standalone: true,
  imports: [NzTableModule],
  template: `
    <nz-table #basicTable [nzData]="dataSet">
      <thead>
        <tr>
          <th>Name</th>
          <th>Age</th>
          <th>Address</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let data of basicTable.data">
          <td>{{ data.name }}</td>
          <td>{{ data.age }}</td>
          <td>{{ data.address }}</td>
        </tr>
      </tbody>
    </nz-table>
  `
})
export class TableExampleComponent {
  dataSet = [
    {
      key: '1',
      name: 'John Brown',
      age: 32,
      address: 'New York No. 1 Lake Park'
    },
    {
      key: '2',
      name: 'Jim Green',
      age: 42,
      address: 'London No. 1 Lake Park'
    }
  ];
}
```

#### 表单组件
```typescript
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';

@Component({
  selector: 'app-form-example',
  standalone: true,
  imports: [NzFormModule, NzInputModule, NzButtonModule],
  template: `
    <form nz-form [formGroup]="validateForm" (ngSubmit)="submitForm()">
      <nz-form-item>
        <nz-form-label nzRequired>Username</nz-form-label>
        <nz-form-control>
          <input nz-input formControlName="userName" placeholder="Username" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-label nzRequired>Password</nz-form-label>
        <nz-form-control>
          <input nz-input type="password" formControlName="password" placeholder="Password" />
        </nz-form-control>
      </nz-form-item>
      <nz-form-item>
        <nz-form-control>
          <button nz-button nzType="primary">Submit</button>
        </nz-form-control>
      </nz-form-item>
    </form>
  `
})
export class FormExampleComponent {
  validateForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]]
    });
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      console.log('submit', this.validateForm.value);
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
}
```

### 3. 全局配置

```typescript
import { ApplicationConfig } from '@angular/core';
import { NzConfig, provideNzConfig } from 'ng-zorro-antd/core/config';

const ngZorroConfig: NzConfig = {
  message: { nzTop: 120 },
  notification: { nzTop: 240 },
  table: { nzBordered: true }
};

export const appConfig: ApplicationConfig = {
  providers: [provideNzConfig(ngZorroConfig)]
};
```

### 4. 国际化配置

```typescript
import { ApplicationConfig } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { provideNzI18n, en_US } from 'ng-zorro-antd/i18n';

registerLocaleData(en);

export const appConfig: ApplicationConfig = {
  providers: [provideNzI18n(en_US)]
};
```

## 参考资源

本技能包含以下参考文档,位于 `references/` 目录:

1. **llms.txt** - 导航文件,包含所有文档和组件的链接
2. **llms-full.txt** - 完整的英文组件文档,包含实现细节和示例
3. **llms-full-cn.txt** - 完整的中文组件文档

## 常见问题解答

### Q: 如何选择合适的组件?
A: 根据使用场景选择:
- 数据录入: Form, Input, Select, DatePicker 等
- 数据展示: Table, List, Card, Descriptions 等
- 导航: Menu, Breadcrumb, Pagination 等
- 反馈: Message, Notification, Modal 等

### Q: 组件样式不生效怎么办?
A: 确保已正确引入样式文件:
```json
// angular.json
{
  "styles": [
    "node_modules/ng-zorro-antd/ng-zorro-antd.min.css"
  ]
}
```

### Q: 如何实现主题切换?
A: ng-zorro 支持多种主题定制方式:
1. Less 变量定制
2. CSS Variable 动态主题
3. 预定义主题(默认、暗黑、紧凑、阿里云)

详见 `references/llms-full-cn.txt` 中的主题定制章节。

### Q: 数据更新后页面没有变化?
A: ng-zorro 组件默认运行在 OnPush 模式下,需要使用 immutable 方式更新数据:
```typescript
// 正确方式
this.dataSet = [...this.dataSet, newItem];

// 错误方式
this.dataSet.push(newItem); // 不会触发变更检测
```

## 最佳实践

1. **按需引入组件模块** - 只导入需要的组件模块,减小打包体积
2. **使用独立组件(Standalone)** - 推荐使用 Angular 的 standalone 组件
3. **配置全局默认值** - 使用全局配置减少重复代码
4. **遵循 Angular 风格指南** - 保持代码风格一致
5. **使用 TypeScript 类型** - 充分利用 TypeScript 的类型系统

## 技术支持

- 官方文档: https://ng.ant.design
- GitHub: https://github.com/NG-ZORRO/ng-zorro-antd
- Stack Overflow: 使用 `ng-zorro-antd` 标签
- 中文社区: Segment Fault `ng-zorro` 标签

## 版本说明

本技能基于 ng-zorro-antd 最新版本开发,支持 Angular ^21.0.0。ng-zorro-antd 与 @angular/core 保持相同的主版本号。

## 更新日志

- v1.0.0 - 初始版本,包含完整的 ng-zorro 组件库文档和示例
