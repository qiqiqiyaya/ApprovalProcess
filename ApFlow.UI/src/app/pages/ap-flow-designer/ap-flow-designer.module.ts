import { Injector, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgZorroAntdCommonModule } from '../common/ng-zorro-antd-common.module';
import { RootComponent } from './root/root.component';
import { ApFlowDesignerRoutes } from './ap-flow-designer.routing';
import { FlowGraphEditorComponent } from './flow-graph-editor/flow-graph-editor.component';
import { X6FlowGraph } from './services/x6-flow-graph';
import { CustomShapeRegister } from './custom-shape-register';
import { ApproveNodeService } from './services/approve-node.service';
import { AddNodeBtnComponent } from './add-node-btn/add-node-btn.component';
import { ParallelApprovalMergeNodeComponent } from './nodes/parallel-approval-merge-node/parallel-approval-Merge-node.component';
import { ParallelApprovalNodeComponent } from './nodes/parallel-approval-node/parallel-approval-node.component';
import { ApprovalSettingsComponent } from './approval-settings/approval-settings.component';
import { ApproveNodeComponent } from './nodes/approve-node/approve-node.component';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdCommonModule,
    ApFlowDesignerRoutes,
  ],
  declarations: [
    RootComponent,
    FlowGraphEditorComponent,
    AddNodeBtnComponent,
    ApproveNodeComponent,
    FlowGraphEditorComponent,
    ParallelApprovalMergeNodeComponent,
    ParallelApprovalNodeComponent,
    ApprovalSettingsComponent
  ],
  providers: [
    X6FlowGraph,
    ApproveNodeService
  ]
})
export class ApFlowDesignerModule {
}
