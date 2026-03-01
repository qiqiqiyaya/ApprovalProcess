import { Component } from '@angular/core';

@Component({
  selector: 'app-parallel-approval',
  templateUrl: './parallel-approval.component.html',
  styleUrls: ['./parallel-approval.component.css'],
  standalone: false,
})
export class ParallelApprovalComponent {
  nodeId: string = '';
  nodeName: string = '并行审批';
  branchCount: number = 0;

  addBranch(): void {
    this.branchCount++;
    console.log('添加分支，当前分支数：', this.branchCount);
  }

  clear(): void {
    this.branchCount = 0;
    console.log('清除所有分支');
  }
}
