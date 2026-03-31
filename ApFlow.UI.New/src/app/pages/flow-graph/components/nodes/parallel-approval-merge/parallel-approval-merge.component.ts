import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
  selector: 'app-parallel-approval-merge',
  templateUrl: './parallel-approval-merge.component.html',
  styleUrls: ['./parallel-approval-merge.component.css'],
  standalone: false,
})
export class ParallelApprovalMergeComponent implements ComponentNode {

  @Input() node: IFlowNode

  close() {
  }
}
