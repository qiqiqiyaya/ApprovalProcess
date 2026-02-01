import { Component, OnInit } from '@angular/core';
import { NgZorroAntdCommonModule } from "../../../../shared/ng-zorro-antd-common.module";

@Component({
  selector: 'app-parallel-approval',
  templateUrl: './parallel-approval.component.html',
  styleUrls: ['./parallel-approval.component.css'],
  imports: [NgZorroAntdCommonModule]
})
export class ParallelApprovalComponent implements OnInit {
  /** 节点ID */
  nodeId: string = '';
  /** 节点名称 */
  nodeName: string = '并行审批';

  constructor() { }

  ngOnInit() {
  }

  /**
   * 处理节点点击事件
   */
  handleNodeClick(): void {
    console.log('并行审批节点被点击:', this.nodeId);
    // 这里可以添加节点点击后的逻辑，如打开配置面板等
  }

}