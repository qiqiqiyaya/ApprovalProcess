import { Component, inject, Injector, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { Graph, Node as xNode } from '@antv/x6';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { register } from '@antv/x6-angular-shape'
import { NodeOperationService } from './node-operation.service';
import { NodeOperationComponent } from './node-operation/node-operation.component';
import { GraphConstant } from './graph-constant';
import { NodeDescription, NodeType } from './node-description';
import { ParallelApprovalBtnComponent } from './parallel-approval-btn/parallel-approval-btn.component';

@Component({
  selector: 'app-x6-flow-designer',
  templateUrl: './x6-flow-designer.component.html',
  styleUrls: ['./x6-flow-designer.component.css'],
  standalone: false,
})
export class X6FlowDesignerComponent implements OnInit {

  @ViewChild('tplContent', { static: true }) tplContent: any;
  flowOperationModel: NzModalRef;
  @ViewChild('template') template: TemplateRef<{}>

  operation = inject(NodeOperationService);
  modal = inject(NzModalService);
  ref = inject(ViewContainerRef);

  /* 图 */
  graph: Graph;
  startNode: xNode;
  endNode: xNode;
  /* 当前操作的节点 */
  currentOpNode: xNode;


  constructor(private injector: Injector) {
    register({
      shape: 'operation-node',
      width: 120,
      height: 20,
      content: NodeOperationComponent,
      injector: this.injector,
    });
    
    register({
      shape: 'parallel-approval-node',
      width: 120,
      height: 40,
      content: ParallelApprovalBtnComponent,
      injector: this.injector,
    })
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.graph = new Graph({
      container: document.getElementById('container')!,
      grid: true,
      panning: true,
      mousewheel: true,
    });
    this.operation.init(this.graph);

    this.addStartNode();
    this.addOperationNode();
    this.addEndNode();

    const current: NodeDescription = { type: NodeType.Start, current: this.startNode, next: [this.currentOpNode] };
    this.startNode.setData(current);
    const opdes: NodeDescription = { type: NodeType.Add, current: this.currentOpNode, prev: this.startNode, next: [this.endNode] };
    this.currentOpNode.setData(opdes);
    const endDes: NodeDescription = { type: NodeType.End, current: this.startNode, prev: this.currentOpNode };
    this.endNode.setData(endDes);

    this.operation.start = this.startNode;
    this.operation.fristOp = this.currentOpNode;
    this.operation.end = this.endNode;

    this.operation.connectOpNode(this.startNode, this.currentOpNode);
    this.operation.connectOpNode(this.currentOpNode, this.endNode);

    this.graph.centerContent();

    this.graph.on('node:click', ({ e, x, y, node, view }) => {
      const nodeData = node.getData() as NodeDescription;
      if (nodeData.type == NodeType.Add) {
        this.operation.crrentOp = nodeData.current;
        this.nodeCreate();
      }
    });
  }

  nodeCreate() {
    this.flowOperationModel = this.modal.create({
      nzTitle: "添加",
      nzContent: this.tplContent,
      nzFooter: null
    });
  }

  openDrawer() {
    this.operation.drawerShow = true;
    this.flowOperationModel.close();
  }

  closeDrawer() {
    this.operation.drawerShow = false;
  }

  addStartNode() {
    this.startNode = this.graph.addNode({ id: "start", width: GraphConstant.nodeWidth, height: GraphConstant.nodeHeight, label: "发起人" });
  }

  addOperationNode() {
    this.currentOpNode = this.graph.addNode({
      id: 'add',
      shape: 'operation-node',
      width: GraphConstant.nodeWidth,
      height: 40,
      label: '结束'
    });
  }

  addEndNode() {
    this.endNode = this.graph.addNode({
      id: 'end',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: '结束'
    });
  }
}
