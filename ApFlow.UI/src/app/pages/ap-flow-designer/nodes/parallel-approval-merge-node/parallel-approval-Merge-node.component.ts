import { Component, OnInit } from '@angular/core';

/**
 * 并行审批合并节点组件
 * 用于在流程图中表示并行审批合并节点
 */
@Component({
  selector: 'app-parallel-approval-Merge-node',
  templateUrl: './parallel-approval-Merge-node.component.html',
  styleUrls: ['./parallel-approval-Merge-node.component.css'],
  standalone: false
})
export class ParallelApprovalMergeNodeComponent implements OnInit {
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
