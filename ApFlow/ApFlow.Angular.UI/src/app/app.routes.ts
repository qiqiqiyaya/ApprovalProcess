import { Routes } from '@angular/router';
import { FormComponent } from './pages/form/form.component';
import { FlowDesignerComponent } from './pages/flow-designer/flow-designer.component';
import { AllNodeComponent } from './pages/all-node/all-node.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/welcome' },
  { path: 'form', component:FormComponent },
  { path: 'flow-designer', component:FlowDesignerComponent },
  { path: 'x6-designer', loadChildren: () => import('./pages/x6-flow-designer/x6-flow-designer.module').then(m => m.X6FlowDesignerModule)  },
  { path: 'all-node', component:AllNodeComponent },
  { path: 'welcome', loadChildren: () => import('./pages/welcome/welcome.routes').then(m => m.WELCOME_ROUTES) }
];
