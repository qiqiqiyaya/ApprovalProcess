import { Component,  OnInit, inject } from '@angular/core';
import { ApproveNodeService } from '../services/approve-node.service';
import { Graph, Node as XNode } from '@antv/x6';

@Component({
  selector: 'app-approve-node',
  templateUrl: './approve-node.component.html',
  styleUrls: ['./approve-node.component.css'],
  standalone: false
})
export class ApproveNodeComponent implements OnInit {

  approveNode = inject(ApproveNodeService);
  
  constructor() { }

  ngOnInit() {
    
  }

  close() {
    this.approveNode.remove();
  }
}
