import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgZorroAntdCommonModule } from '../common/ng-zorro-antd-common.module';
import { RootComponent } from './root/root.component';
import { ApFlowDesignerRoutes } from './ap-flow-designer.routing';
import { FlowGraphEditorComponent } from './flow-graph-editor/flow-graph-editor.component';
import { X6FlowGraph } from './services/x6-flow-graph';
import { ApFlowComponentsModule } from '../ap-flow-components/ap-flow-components.module';

@NgModule({
  imports: [
    CommonModule,
    NgZorroAntdCommonModule,
    ApFlowDesignerRoutes,
    ApFlowComponentsModule
  ],
  declarations: [
    RootComponent,
    FlowGraphEditorComponent
  ],
  providers:[
    X6FlowGraph
  ]
})
export class ApFlowDesignerModule { }
