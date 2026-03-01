import { Routes } from '@angular/router';
import { EditorComponent } from './pages/flow-graph/components/editor/editor.component';
import { CreateComponent } from './pages/business/create/create.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/create' },
  { path: 'create', component: CreateComponent }
];
