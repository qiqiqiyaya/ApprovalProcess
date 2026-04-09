import { Routes, RouterModule } from '@angular/router';
import { CountersignatureComponent } from './countersignature-会签/countersignature.component';
import { OrApproalsComponent } from './or-approals-或签/or-approals.component';
import { ParallelApprovalComponent } from './parallel-approval-并行审批/parallel-approval.component';

const routes: Routes = [
  { path: '', component: CountersignatureComponent },
  { path: 'countersignature', component: CountersignatureComponent },
  { path: 'or-approals', component: OrApproalsComponent },
  { path: 'parallel-approval', component: ParallelApprovalComponent },
];

export const TestCaseRoutes = RouterModule.forChild(routes);
