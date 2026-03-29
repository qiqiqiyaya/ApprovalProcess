import { Component, Input } from '@angular/core';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
  selector: 'app-parallel-approval-merge',
  templateUrl: './parallel-approval-merge.component.html',
  styleUrls: ['./parallel-approval-merge.component.css'],
  standalone: false,
})
export class ParallelApprovalMergeComponent {
  nodeId: string = '';
  nodeName: string = '合并审批';
  @Input() node: IFlowNode
}
