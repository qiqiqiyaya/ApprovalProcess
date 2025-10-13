import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { X6FlowDesignerComponent } from './x6-flow-designer.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NodeOperationComponent } from './node-operation/node-operation.component';
import { AddNodeComponent } from './add-node/add-node.component';
import { NodeOperationService } from './node-operation.service';
import { X6FlowDesignerRoutes } from './x6-flow-designer.routing';
import { ParallelApprovalBtnComponent } from './parallel-approval-btn/parallel-approval-btn.component';
import { X6FlowGraph } from './services/x6-flow-graph';
import { ApproveNodeService } from './services/approve-node.service';
import { ApproveNodeComponent } from './approve-node/approve-node.component';
import { NzGridModule } from 'ng-zorro-antd/grid';

@NgModule({
  imports: [
    CommonModule,
    NzButtonModule,
    NzModalModule,
    NzCardModule,
    NzIconModule,
    NzDrawerModule,
    NzGridModule,
    X6FlowDesignerRoutes
  ],
  declarations: [
    X6FlowDesignerComponent,
    NodeOperationComponent,
    AddNodeComponent,
    ParallelApprovalBtnComponent,
    ApproveNodeComponent
  ],
  providers: [
    NodeOperationService,
    X6FlowGraph,
    ApproveNodeService
  ]
})
export class X6FlowDesignerModule { }
