import { Component, inject, OnInit } from '@angular/core';
import { GraphManagerService } from '../../../services/graph-manager.service';

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  standalone: false,
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;
  private graphManager = inject(GraphManagerService);
  constructor() {
  }

  ngOnInit() {
  }

  addApprove() {
    var node = this.graphManager.currentNode;
    this.graphManager.addApproveNode(node);
    this.graphManager.render();
  }

  addParallelApproval() {
    var node = this.graphManager.currentNode;
    this.graphManager.addBranch(node);
    this.graphManager.render();
  }
}
