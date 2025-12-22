import { Routes, RouterModule } from '@angular/router';
import { AllNodeComponent } from './all-node/all-node.component';
import { FormComponent } from './form/form.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/x6-designer' },
  { path: 'x6-designer', loadChildren: () => import('./x6-flow-designer/x6-flow-designer.module').then(m => m.X6FlowDesignerModule) },
  { path: 'form', component: FormComponent },
  { path: 'all-node', component: AllNodeComponent },
  { path: 'custom-ap-flow', loadChildren: () => import('./custom-ap-flow/custom-ap-flow.module').then(m => m.CustomApFlowModule) },
];

export const PagesRoutes = RouterModule.forChild(routes);
