import { Component, inject, Input } from '@angular/core';
import { NzDrawerPlacement, NzDrawerService } from 'ng-zorro-antd/drawer';
import { ApprovalSettingsComponent } from '../../approval-settings/approval-settings.component';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
  selector: 'app-approve',
  templateUrl: './approve.component.html',
  styleUrls: ['./approve.component.css'],
  standalone: false,
})
export class ApproveComponent implements ComponentNode {

  @Input() node: IFlowNode

  drawer = inject(NzDrawerService);

  close() {
  }

  setApprove() {
    this.drawer.create<ApprovalSettingsComponent, { afterClose: () => void; }>({
      nzTitle: '设置审批人',
      nzWidth: 700,
      nzContent: ApprovalSettingsComponent,
      nzMaskClosable: true
    });
  }
}
