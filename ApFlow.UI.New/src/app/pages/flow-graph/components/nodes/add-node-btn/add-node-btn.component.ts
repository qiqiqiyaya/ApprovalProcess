import { Component, inject, OnInit } from '@angular/core';
import { EditorService } from '../../../services/editor.service';

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  standalone: false,
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;
  private editor = inject(EditorService);
  constructor() {
  }

  ngOnInit() {
  }

  addApprove() {
    var node = this.editor.getFolwNode();
    this.editor.flowGraph().addApproveNode(node);
    this.editor.renderGraph();
  }

  addParallelApproval() {
    var node = this.editor.getFolwNode();
    this.editor.flowGraph().addBranch(node);
    this.editor.renderGraph();
  }
}
