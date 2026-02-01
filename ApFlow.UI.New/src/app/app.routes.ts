import { Routes } from '@angular/router';
import { EditorComponent } from './pages/flow-graph/editor/editor.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/editor' },
  { path: 'editor', component:EditorComponent },
  { path: 'welcome', loadChildren: () => import('./pages/welcome/welcome.routes').then(m => m.WELCOME_ROUTES) }
];
