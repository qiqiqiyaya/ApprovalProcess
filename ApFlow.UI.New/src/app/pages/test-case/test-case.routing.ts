import { Routes, RouterModule } from '@angular/router';
import { CountersignatureComponent } from './countersignature-会签/countersignature.component';
import { OrApproalsComponent } from './or-approals-或签/or-approals.component';

const routes: Routes = [
  { path: '', component: CountersignatureComponent },
  { path: 'countersignature', component: CountersignatureComponent },
  { path: 'or-approals', component: OrApproalsComponent },
];

export const TestCaseRoutes = RouterModule.forChild(routes);
