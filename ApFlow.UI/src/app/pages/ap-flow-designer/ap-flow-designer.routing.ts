import { Routes, RouterModule } from '@angular/router';
import { FlowGraphEditorComponent } from './flow-graph-editor/flow-graph-editor.component';

const routes: Routes = [
  { path: '', component: FlowGraphEditorComponent },
  { path: 'flow-graph-editor', component: FlowGraphEditorComponent },
];

export const ApFlowDesignerRoutes = RouterModule.forChild(routes);
