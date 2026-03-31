import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
  selector: 'app-parallel-approval',
  templateUrl: './parallel-approval.component.html',
  styleUrls: ['./parallel-approval.component.css'],
  standalone: false,
})
export class ParallelApprovalComponent implements ComponentNode {

  @Input() node: IFlowNode

  branchCount = 0

  clear() {
    this.branchCount = 0
  }

  addBranch() {
    this.branchCount++
  }

  close() {
  }
}
