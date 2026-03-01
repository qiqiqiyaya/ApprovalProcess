import { Component } from '@angular/core';

@Component({
  selector: 'app-parallel-approval-merge',
  templateUrl: './parallel-approval-merge.component.html',
  styleUrls: ['./parallel-approval-merge.component.css'],
  standalone: false,
})
export class ParallelApprovalMergeComponent {
  nodeId: string = '';
  nodeName: string = '合并审批';
}
