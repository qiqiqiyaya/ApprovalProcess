import { Injector, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgZorroAntdCommonModule } from '../common/ng-zorro-antd-common.module';
import { RootComponent } from './root/root.component';
import { ApFlowDesignerRoutes } from './ap-flow-designer.routing';
import { FlowGraphEditorComponent } from './flow-graph-editor/flow-graph-editor.component';
import { X6FlowGraph } from './services/x6-flow-graph';
import { ApproveNodeService } from './services/approve-node.service';
import { AddNodeBtnComponent } from './add-node-btn/add-node-btn.component';
import { ApproveNodeComponent } from './nodes/approve-node/approve-node.component';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdCommonModule,
    ApFlowDesignerRoutes
  ],
  declarations: [
    RootComponent,
    FlowGraphEditorComponent,
    AddNodeBtnComponent,
    ApproveNodeComponent
  ],
  providers: [
    X6FlowGraph,
    ApproveNodeService
  ]
})
export class ApFlowDesignerModule {
}
