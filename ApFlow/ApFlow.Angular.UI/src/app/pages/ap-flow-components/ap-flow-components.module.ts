import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNodeBtnComponent } from './add-node-btn/add-node-btn.component';
import { NgZorroAntdCommonModule } from '../common/ng-zorro-antd-common.module';
import { ApproveNodeComponent } from './approve-node/approve-node.component';
import { ParallelApprovalNodeComponent } from './parallel-approval-node/parallel-approval-node.component';
import { ParallelApprovalMergeNodeComponent } from './parallel-approval-Merge-node/parallel-approval-Merge-node.component';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdCommonModule
  ],
  declarations: [
    AddNodeBtnComponent,
    ApproveNodeComponent,
    ParallelApprovalNodeComponent,
    ParallelApprovalMergeNodeComponent
  ],
  exports:[
    AddNodeBtnComponent,
    ApproveNodeComponent,
    ParallelApprovalNodeComponent,
    ParallelApprovalMergeNodeComponent
  ]
})
export class ApFlowComponentsModule { }
