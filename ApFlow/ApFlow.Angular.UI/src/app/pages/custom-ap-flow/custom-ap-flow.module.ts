import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// NG-ZORRO 组件
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';

// 自定义组件
import { CustomApFlowComponent } from './custom-ap-flow.component';
import { NodePropertiesComponent } from './components/node-properties/node-properties.component';
import { EdgePropertiesComponent } from './components/edge-properties/edge-properties.component';

// 节点组件
import { ApproverNodeComponent } from './components/nodes/approver-node.component';
import { ParallelNodeComponent } from './components/nodes/parallel-node.component';
import { CcNodeComponent } from './components/nodes/cc-node.component';
import { ConditionNodeComponent } from './components/nodes/condition-node.component';
import { DynamicConditionNodeComponent } from './components/nodes/dynamic-condition-node.component';
import { ConditionParallelNodeComponent } from './components/nodes/condition-parallel-node.component';

// 自定义服务
import { CustomApFlowService } from './services/custom-ap-flow.service';

// 自定义路由
import { CustomApFlowRoutingModule } from './custom-ap-flow.routing';

@NgModule({
    declarations: [
        CustomApFlowComponent,
        NodePropertiesComponent,
        EdgePropertiesComponent,
        ApproverNodeComponent,
        ParallelNodeComponent,
        CcNodeComponent,
        ConditionNodeComponent,
        DynamicConditionNodeComponent,
        ConditionParallelNodeComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        CustomApFlowRoutingModule,

        // NG-ZORRO 模块
        NzButtonModule,
        NzCardModule,
        NzFormModule,
        NzInputModule,
        NzInputNumberModule,
        NzSelectModule,
        NzCheckboxModule,
        NzRadioModule,
        NzIconModule,
        NzDividerModule,
        NzDrawerModule,
        NzModalModule,
        NzMessageModule,
        NzNotificationModule,
        NzTabsModule,
        NzTableModule,
        NzTagModule
    ],
    providers: [
        CustomApFlowService
    ],
    exports: [
        CustomApFlowComponent,
        NodePropertiesComponent,
        EdgePropertiesComponent,
        ApproverNodeComponent,
        ParallelNodeComponent,
        CcNodeComponent,
        ConditionNodeComponent,
        DynamicConditionNodeComponent,
        ConditionParallelNodeComponent
    ]
})
export class CustomApFlowModule { }