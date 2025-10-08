import { Component, inject, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NodeOperationService } from '../node-operation.service';
import { GraphConstant } from '../graph-constant';

@Component({
  selector: 'app-add-node',
  templateUrl: './add-node.component.html',
  styleUrls: ['./add-node.component.css'],
  standalone: false
})
export class AddNodeComponent implements OnInit {

  operation = inject(NodeOperationService);
  constructor() { }

  ngOnInit() {
  }

  create() {
    this.operation.addApproveNode({
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });
  }

  parallel() {
    this.operation.parallelApproval({
      shape: 'parallel-approval-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "添加审批节点"
    });
  }
}
