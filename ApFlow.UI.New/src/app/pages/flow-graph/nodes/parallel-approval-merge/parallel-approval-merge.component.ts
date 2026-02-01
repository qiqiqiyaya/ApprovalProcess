import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-parallel-approval-merge',
  templateUrl: './parallel-approval-merge.component.html',
  styleUrls: ['./parallel-approval-merge.component.css']
})
export class ParallelApprovalMergeComponent implements OnInit {
  /** 节点ID */
  nodeId: string = '';
  /** 节点名称 */
  nodeName: string = '合并审批';

  constructor() { }

  ngOnInit() {
  }

  /**
   * 处理节点点击事件
   */
  handleNodeClick(): void {
    console.log('合并审批节点被点击:', this.nodeId);
    // 这里可以添加节点点击后的逻辑，如打开配置面板等
  }

}
