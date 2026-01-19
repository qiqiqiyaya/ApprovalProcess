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
import { FixedLayoutService } from './services/fixed-layout.service';
import { LayoutService } from './services/layout.service';
import { GenerateNodeService } from './services/generate-node.service';
import { FlowGraph } from './services/flow-graph';

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
    // X6FlowGraph,
    // ApproveNodeService,
    FlowGraph,
    FixedLayoutService,
    LayoutService,
    GenerateNodeService
  ]
})
export class ApFlowDesignerModule {
}
