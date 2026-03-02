import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorComponent } from './components/editor/editor.component';
import { EditorService } from './services/editor.service';
import { AddNodeBtnComponent } from './components/nodes/add-node-btn/add-node-btn.component';
import { ApproveComponent } from './components/nodes/approve/approve.component';
import { ParallelApprovalComponent } from './components/nodes/parallel-approval/parallel-approval.component';
import { ParallelApprovalMergeComponent } from './components/nodes/parallel-approval-merge/parallel-approval-merge.component';
import { ApprovalSettingsComponent } from './components/approval-settings/approval-settings.component';
import { LayoutConfigComponent } from './components/layout-config/layout-config.component';
import { LayoutConfigService } from './services/layout/layout-config.service';
import { LayoutService } from './services/layout/layout.service';
import { NgZorroAntdCommonModule } from '../../shared/ng-zorro-antd-common.module';

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
    ApprovalSettingsComponent,
    LayoutConfigComponent
  ],
  providers: [
    EditorService,
    LayoutConfigService,
    LayoutService,
    // Provide localStorage for LayoutConfigService
    { provide: 'LOCALSTORAGE', useValue: window.localStorage },
  ],
  exports: [
    EditorComponent
  ]
})
export class FlowGraphModule { }
