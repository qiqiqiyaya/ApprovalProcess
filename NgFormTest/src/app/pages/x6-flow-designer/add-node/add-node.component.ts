import { Component, inject, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NodeOperationService } from '../node-operation.service';
import { GraphConstant } from '../graph-constant';
import { ApproveNodeService } from '../services/approve-node.service';
import { ParallelApproveNodeService } from '../services/parallel-approve-node.service';

@Component({
  selector: 'app-add-node',
  templateUrl: './add-node.component.html',
  styleUrls: ['./add-node.component.css'],
  standalone: false
})
export class AddNodeComponent implements OnInit {

  operation = inject(NodeOperationService);

  approveNode = inject(ApproveNodeService);
  parallelApproveNode = inject(ParallelApproveNodeService);
  constructor() { }

  ngOnInit() {
  }

  addApproveNode() {
    this.approveNode.add();
  }

  addParallelApproveNode() {
    this.parallelApproveNode.add();
  }
}
