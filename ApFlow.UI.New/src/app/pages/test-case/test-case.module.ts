import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestCaseRoutes } from './test-case.routing';
import { FlowGraphModule } from '../flow-graph/flow-graph.module';
import { NgZorroAntdCommonModule } from '../../shared/ng-zorro-antd-common.module';
import { CountersignatureComponent } from './countersignature-会签/countersignature.component';
import { OrApproalsComponent } from './or-approals-或签/or-approals.component';

@NgModule({
  imports: [
    CommonModule,
    TestCaseRoutes,
    FlowGraphModule,
    NgZorroAntdCommonModule
  ],
  declarations: [
    CountersignatureComponent,
    OrApproalsComponent
  ]
})
export class TestCaseModule { }
