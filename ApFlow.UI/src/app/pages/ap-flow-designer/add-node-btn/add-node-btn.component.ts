import { Component, OnInit } from '@angular/core';

// 节点类型定义
interface ApprovalNode {
  type: string;       // 节点类型标识
  name: string;       // 节点名称
  description: string; // 节点描述
  icon: string;       // 节点图标
}

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  standalone: false
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;

  // 审批节点列表（从nodes目录获取的节点）
  approvalNodes: ApprovalNode[] = [];

  // 选中的节点
  selectedNode: ApprovalNode | null = null;

  constructor() { }

  ngOnInit() {
    // 初始化审批节点列表
    this.initApprovalNodes();
  }

  /**
   * 初始化审批节点列表
   */
  initApprovalNodes(): void {
    // 从nodes目录获取所有可用的审批节点
    this.approvalNodes = [
      {
        type: 'parallel-approval',
        name: '并行审批',
        description: '多个审批人同时审批，全部通过后流程继续',
        icon: 'team'
      },
      {
        type: 'parallel-approval-merge',
        name: '合并审批',
        description: '合并多个并行审批流程分支',
        icon: 'merge-cells'
      },
      {
        type: 'approve',
        name: '普通审批',
        description: '单个审批人审批',
        icon: 'check-circle'
      }
    ];
  }

  /**
   * 处理添加节点按钮点击事件
   */
  onAddClick(): void {
    // 显示模态框
    this.isModalVisible = true;
  }

  /**
   * 处理节点选择
   */
  onNodeSelect(node: ApprovalNode): void {
    this.selectedNode = node;
  }

  /**
   * 处理模态框关闭事件
   */
  handleModalCancel(): void {
    this.isModalVisible = false;
    this.selectedNode = null; // 重置选择
  }

  /**
   * 处理模态框确认事件
   */
  handleModalOk(): void {
    if (this.selectedNode) {
      // 处理节点添加逻辑
      console.log('添加节点:', this.selectedNode);
      // 这里可以添加向流程设计器发送节点添加事件的逻辑
      // 目前仅在控制台打印信息
    }
    this.isModalVisible = false;
    this.selectedNode = null; // 重置选择
  }

}
