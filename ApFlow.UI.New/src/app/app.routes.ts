import { Routes } from '@angular/router';
import { CreateComponent } from './pages/business/create/create.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/create' },
  { path: 'create', component: CreateComponent }
];
