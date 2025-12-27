import { Component, OnInit, inject } from '@angular/core';
import { ApproveNodeService } from '../../x6-flow-designer/services/approve-node.service';
import { Graph, Node as XNode } from '@antv/x6';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { ApprovalSettingsComponent } from '../../x6-flow-designer/approval-settings/approval-settings.component';

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
      nzMaskClosable:false,
      nzTitle: '设置审批人',
      nzWidth: 800,
      nzContent: ApprovalSettingsComponent,
    })
  }
}
