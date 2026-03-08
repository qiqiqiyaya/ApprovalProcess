import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorComponent } from './components/editor/editor.component';
import { AddNodeBtnComponent } from './components/nodes/add-node-btn/add-node-btn.component';
import { ApproveComponent } from './components/nodes/approve/approve.component';
import { ParallelApprovalComponent } from './components/nodes/parallel-approval/parallel-approval.component';
import { ParallelApprovalMergeComponent } from './components/nodes/parallel-approval-merge/parallel-approval-merge.component';
import { ApprovalSettingsComponent } from './components/approval-settings/approval-settings.component';
import { NgZorroAntdCommonModule } from '../../shared/ng-zorro-antd-common.module';
import { GraphManagerService } from './services/graph-manager.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgZorroAntdCommonModule
  ],
  declarations: [
    EditorComponent,
    AddNodeBtnComponent,
    ApproveComponent,
    ParallelApprovalComponent,
    ParallelApprovalMergeComponent,
    ApprovalSettingsComponent
  ],
  providers: [
    GraphManagerService
  ],
  exports: [
    EditorComponent
  ]
})
export class FlowGraphModule { }
