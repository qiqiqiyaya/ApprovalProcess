import { Component, OnInit, inject } from '@angular/core';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { NgZorroAntdCommonModule } from '../../../../shared/ng-zorro-antd-common.module';

@Component({
  selector: 'app-approve',
  imports:[NgZorroAntdCommonModule],
  templateUrl: './approve.component.html',
  styleUrls: ['./approve.component.css']
})
export class ApproveComponent implements OnInit {

  drawer = inject(NzDrawerService);

  constructor() { }

  ngOnInit() {

  }

  close() {
  }

  setApprove() {
    // this.drawer.create({
    //   nzMaskClosable: false,
    //   nzTitle: '设置审批人',
    //   nzWidth: 800,
    //   nzContent: ApprovalSettingsComponent,
    // })
  }
}
