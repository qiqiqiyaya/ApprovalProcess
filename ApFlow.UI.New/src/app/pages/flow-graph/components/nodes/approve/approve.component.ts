import { Component, inject } from '@angular/core';
import { NzDrawerPlacement, NzDrawerService } from 'ng-zorro-antd/drawer';
import { ApprovalSettingsComponent } from '../../approval-settings/approval-settings.component';

@Component({
  selector: 'app-approve',
  templateUrl: './approve.component.html',
  styleUrls: ['./approve.component.css'],
  standalone: false,  
})
export class ApproveComponent {

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
