import { Component, inject, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NodeOperationService } from '../node-operation.service';
import { GraphConstant } from '../graph-constant';
import { ApproveNodeService } from '../services/approve-node.service';

@Component({
  selector: 'app-add-node',
  templateUrl: './add-node.component.html',
  styleUrls: ['./add-node.component.css'],
  standalone: false
})
export class AddNodeComponent implements OnInit {

  operation = inject(NodeOperationService);
  approveNode = inject(ApproveNodeService);
  constructor() { }

  ngOnInit() {
  }

  create() {
    this.approveNode.addApproveNode();
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
