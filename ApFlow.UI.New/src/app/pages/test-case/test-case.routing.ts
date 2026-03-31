import { Routes, RouterModule } from '@angular/router';
import { CountersignatureComponent } from './countersignature-会签/countersignature.component';

const routes: Routes = [
  { path: '', component: CountersignatureComponent },
  { path: 'countersignature', component: CountersignatureComponent },
];

export const TestCaseRoutes = RouterModule.forChild(routes);
