import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EditorComponent } from './components/editor/editor.component';
import { EditorService } from './services/editor.service';
import { AddNodeBtnComponent } from './components/nodes/add-node-btn/add-node-btn.component';
import { ApproveComponent } from './components/nodes/approve/approve.component';
import { ParallelApprovalComponent } from './components/nodes/parallel-approval/parallel-approval.component';
import { ParallelApprovalMergeComponent } from './components/nodes/parallel-approval-merge/parallel-approval-merge.component';
import { ApprovalSettingsComponent } from './components/approval-settings/approval-settings.component';
import { NgZorroAntdCommonModule } from '../../shared/ng-zorro-antd-common.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
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
    EditorService,
  ],
  exports: [
    EditorComponent
  ]
})
export class FlowGraphModule { }
