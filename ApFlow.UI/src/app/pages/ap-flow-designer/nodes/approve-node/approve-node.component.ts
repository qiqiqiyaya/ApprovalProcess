import { Component, OnInit, inject } from '@angular/core';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { ApprovalSettingsComponent } from '../../approval-settings/approval-settings.component';
import { ApproveNodeService } from '../../services/approve-node.service';

@Component({
  selector: 'app-approve-node',
  templateUrl: './approve-node.component.html',
  styleUrls: ['./approve-node.component.css'],
  standalone: false
})
export class ApproveNodeComponent implements OnInit {

  approveNode = inject(ApproveNodeService);
  drawer = inject(NzDrawerService);

  constructor() { }

  ngOnInit() {

  }

  close() {
    this.approveNode.remove();
  }

  setApprove() {

    this.drawer.create({
      nzMaskClosable: false,
      nzTitle: '设置审批人',
      nzWidth: 800,
      nzContent: ApprovalSettingsComponent,
    })
  }
}
