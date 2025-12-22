import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomApFlowComponent } from './custom-ap-flow.component';

const routes: Routes = [
    {
        path: '',
        component: CustomApFlowComponent,
        data: {
            title: '自定义流程图',
            breadcrumb: '自定义流程图'
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CustomApFlowRoutingModule { }