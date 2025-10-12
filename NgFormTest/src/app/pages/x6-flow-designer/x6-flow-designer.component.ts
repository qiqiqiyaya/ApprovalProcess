import { Component, inject, Injector, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { Graph, Node as XNode } from '@antv/x6';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { register } from '@antv/x6-angular-shape'
import { NodeOperationService } from './node-operation.service';
import { NodeOperationComponent } from './node-operation/node-operation.component';
import { GraphConstant } from './graph-constant';
import { NodeInfo, NodeType } from './node-description';
import { ParallelApprovalBtnComponent } from './parallel-approval-btn/parallel-approval-btn.component';
import { X6FlowGraph } from './services/x6-flow-graph';
import { X6NodeRegister } from './x6-node-register';

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

  startNode: XNode;
  endNode: XNode;
  /* 当前操作的节点 */
  currentOpNode: XNode;


  constructor(injector: Injector,
    private flowGraph: X6FlowGraph) {
    X6NodeRegister.register(injector);
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    const graph = new Graph({
      container: document.getElementById('container')!,
      grid: true,
      panning: true,
      mousewheel: true,
    });

    const startNode = graph.addNode({ id: "start", width: GraphConstant.nodeWidth, height: GraphConstant.nodeHeight, label: "发起人" });
    const startInfo: NodeInfo = { type: NodeType.Start, current: startNode, next: [] };

    const operationNode = graph.addNode({ id: 'add', shape: 'operation-node', width: GraphConstant.nodeWidth, height: 40, label: '结束' });
    const operationNodeInfo: NodeInfo = { type: NodeType.AddApproveNode, current: operationNode, prev: startNode, next: [] };

    const endNode = graph.addNode({ id: 'end', width: GraphConstant.nodeWidth, height: GraphConstant.nodeHeight, label: '结束' });
    const endNodeInfo: NodeInfo = { type: NodeType.End, current: endNode, prev: operationNode };
    
    startInfo.next?.push(operationNode);
    operationNodeInfo.next?.push(endNode);

    startNode.setData(startInfo);
    operationNode.setData(operationNodeInfo);
    endNode.setData(endNodeInfo);


    // this.operation.connectOpNode(this.startNode, this.currentOpNode);
    // this.operation.connectOpNode(this.currentOpNode, this.endNode);

    // this.flowGraph.graph.on('node:click', ({ e, x, y, node, view }) => {
    //   const nodeData = node.getData() as NodeInfo;
    //   if (nodeData.type == NodeType.AddApproveNode) {
    //     this.operation.crrentOp = nodeData.current;
    //     this.nodeCreate();
    //   }
    // });


    this.flowGraph.init(graph, startNode, endNode);
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
    this.startNode = this.flowGraph.graph.addNode({ id: "start", width: GraphConstant.nodeWidth, height: GraphConstant.nodeHeight, label: "发起人" });
  }

  addOperationNode() {
    return this.flowGraph.graph.addNode({
      id: 'add',
      shape: 'operation-node',
      width: GraphConstant.nodeWidth,
      height: 40,
      label: '结束'
    });
  }
}
