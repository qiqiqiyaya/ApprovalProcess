import { Routes, RouterModule } from '@angular/router';
import { X6FlowDesignerComponent } from './x6-flow-designer.component';

const routes: Routes = [
  { path: '', component:X6FlowDesignerComponent },
];

export const X6FlowDesignerRoutes = RouterModule.forChild(routes);
