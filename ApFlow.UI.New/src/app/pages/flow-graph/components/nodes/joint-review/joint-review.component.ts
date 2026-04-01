import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
  selector: 'app-joint-review',
  templateUrl: './joint-review.component.html',
  styleUrls: ['./joint-review.component.css'],
  standalone: false,
})
export class JointReviewComponent implements ComponentNode {

  @Input() node: IFlowNode

  close() {
  }
}