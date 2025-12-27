import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { X6FlowDesignerComponent } from './x6-flow-designer.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { AddNodeComponent } from '../ap-flow-node-components/add-node/add-node.component';
import { NodeOperationService } from './node-operation.service';
import { X6FlowDesignerRoutes } from './x6-flow-designer.routing';
import { X6FlowGraph } from './services/x6-flow-graph';
import { ApproveNodeService } from './services/approve-node.service';
import { ApproveNodeComponent } from '../ap-flow-components/approve-node/approve-node.component';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { ParallelApproveNodeService } from './services/parallel-approve-node.service';
import { ParallelApprovalNodeComponent } from '../ap-flow-components/parallel-approval-node/parallel-approval-node.component';
import { ParallelApprovalMergeNodeComponent } from '../ap-flow-components/parallel-approval-Merge-node/parallel-approval-Merge-node.component';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { ApprovalSettingsComponent } from './approval-settings/approval-settings.component';
import { ApproverConfigurationComponent } from './approval-settings/approver-configuration/approver-configuration.component';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ApprovalSettingsService } from './services/approval-settings.service';
import { FormSettingsComponent } from './form-settings/form-settings.component';
import { DelonFormModule } from '@delon/form';
import { FlowGraphComponent } from './flow-graph/flow-graph.component';
import { FormFieldConfigurationComponent } from './approval-settings/form-field-configuration/form-field-configuration.component';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FlowService } from './services/flow.service';
import { FlowFormService } from './services/flow-form.service';

@NgModule({
  imports: [
    CommonModule,
    NzButtonModule,
    NzModalModule,
    NzCardModule,
    NzIconModule,
    NzDrawerModule,
    NzGridModule,
    NzTabsModule,
    NzRadioModule,
    ReactiveFormsModule,
    FormsModule,
    DelonFormModule,
    NzTableModule,
    
    X6FlowDesignerRoutes
  ],
  declarations: [
    X6FlowDesignerComponent,
    AddNodeComponent,
    ApproveNodeComponent,
    ApprovalSettingsComponent,
    ApproverConfigurationComponent,
    FormSettingsComponent,
    FlowGraphComponent,
    FormFieldConfigurationComponent
  ],
  providers: [
    NodeOperationService,
    X6FlowGraph,
    ApproveNodeService,
    ParallelApproveNodeService,
    ApprovalSettingsService,
    FlowService,
    FlowFormService
  ]
})
export class X6FlowDesignerModule { }
