import { Component, inject, OnInit } from '@angular/core';
import { NgZorroAntdCommonModule } from '../../../../shared/ng-zorro-antd-common.module';
import { EditorService } from '../../services/editor.service';

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  imports: [NgZorroAntdCommonModule]
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;

  // 注入 X6FlowGraph 服务
  // private readonly flowGraph = inject(X6FlowGraph);

  constructor(private editorService: EditorService) {

  }

  ngOnInit() {

  }


  addApprove() {
    debugger;
    const flowGraph = this.editorService.getFlowGraph();
    flowGraph.addApproveNode(flowGraph.nodes[0]);
    this.editorService.refreshGraph();
  }

  addParallelApproval() {

  }
}
