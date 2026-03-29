import { Component, inject, Input, input, OnInit } from '@angular/core';
import { GraphManagerService } from '../../../services/graph-manager.service';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode, NgArguments } from '../../../models/graph-definition';

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  standalone: false,
})
export class AddNodeBtnComponent implements OnInit, ComponentNode {

  // 模态框显示状态
  isModalVisible = false;
  private graphManager = inject(GraphManagerService);

  @Input() node: IFlowNode

  constructor() {
  }

  ngOnInit() {

  }

  addApprove() {
    this.graphManager.addApproveNode(this.node);
    this.graphManager.render();
  }

  addParallelApproval() {
    this.graphManager.addBranch(this.node);
    this.graphManager.render();
  }
}
