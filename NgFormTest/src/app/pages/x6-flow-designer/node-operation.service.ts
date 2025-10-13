import { Injectable, Injector } from '@angular/core';
import { Graph, Node as XNode } from '@antv/x6';
import { GraphConstant } from './graph-constant';
import { NodeInfo, NodeType } from './node-description';
import { NzModalRef } from 'ng-zorro-antd/modal';

type direction = "x" | "xy";

@Injectable()
export class NodeOperationService {

  

  constructor(private injector: Injector) {

  }

  /* 图 */
  private _graph: Graph;
  init(graph: Graph) {
    this._graph = graph;
  }

  public start: XNode;
  public end: XNode;
  public fristOp: XNode;

  /**
   * 操作的添加node
   */
  public crrentOp: XNode;
  /* 模态框 */
  drawerShow = false;


  public yAutoSet(source: XNode, target: XNode) {
    const position = source.getPosition();
    const size = source.getSize();
    const pos = { ...position }
    pos.y += size.height;
    pos.y += GraphConstant.ySpace;
    target.setPosition(pos);
  }

  public xyAutoSet(source: XNode, targets: XNode[]) {
    const position = source.getPosition();
    const size = source.getSize();
    targets.forEach((res, index) => {
      const pos = { ...position };
      pos.y += size.height;
      pos.y += GraphConstant.ySpace;

      if (index / 2 == 0) {
        pos.x += GraphConstant.xSpace;
      }else{
        pos.x -= GraphConstant.xSpace;
      }
      res.setPosition(pos);
    })
  }

  connect(source: XNode, target: XNode) {
    this.yAutoSet(source, target);
    this._graph.addEdge({ source: source.id, target: target.id });
  }

  connectMult(source: XNode, targets: XNode[]) {
    this.xyAutoSet(source, targets);
    targets.forEach(res=>{
      this._graph.addEdge({ source: source.id, target: res.id });
    })
  }

  connectOpNode(source: XNode, target: XNode) {
    this._graph.addEdge({ source: source.id, target: target.id, attrs: { line: { targetMarker: null }, }, });
    this.yAutoSet(source, target);
  }

  addApproveNode(nodeMetaData: XNode.Metadata) {
    const newNode = this._graph.addNode(nodeMetaData);
    const currentOpNode = this.crrentOp;
    const opNodeData = currentOpNode.getData() as NodeInfo;

    // opNodeData.next?.forEach(res => this._graph.removeConnectedEdges(res));
    const edges = this._graph.getOutgoingEdges(currentOpNode);
    edges?.forEach(res => {
      this._graph.removeEdge(res);
    })

    const newOpNode = this._graph.addNode({
      shape: 'operation-node',
      width: GraphConstant.nodeWidth,
      height: 40
    });

    const newNodeDes: NodeInfo = { type: NodeType.Info, current: newNode, prev: opNodeData.current, next: [newOpNode] };
    const newOpdes: NodeInfo = { type: NodeType.AddApproveNode, current: newOpNode, prev: newNode, next: opNodeData.next };
    newNode.setData(newNodeDes);
    newOpNode.setData(newOpdes);

    opNodeData.next = [newNode];
    currentOpNode.setData(opNodeData);

    this.connect(currentOpNode, newNode);
    this.connectOpNode(newNode, newOpNode);
    if (newOpdes.next) {
      this.connect(newOpNode, newOpdes.next[0]);
      this.ReSetPostion(newOpdes.next[0]);
    }
    this.drawerShow = false;
  }

  private ReSetPostion(next: XNode) {
    if (!next) return;

    var data = next.getData() as NodeInfo;
    data.next?.forEach(res => {
      this.yAutoSet(next, res);
      this.ReSetPostion(res);
    });
  }

  parallelApproval(nodeMetaData: XNode.Metadata) {
    const newNode = this._graph.addNode(nodeMetaData);
    const currentOpNode = this.crrentOp;
    const opNodeData = currentOpNode.getData() as NodeInfo;

    const edges = this._graph.getOutgoingEdges(currentOpNode);
    edges?.forEach(res => {
      this._graph.removeEdge(res);
    })

    const newNode1 = this._graph.addNode({
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });
    const newNode2 = this._graph.addNode({
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });


    const newOpNode1 = this._graph.addNode({
      shape: 'operation-node',
      width: GraphConstant.nodeWidth,
      height: 40,
    });
    const newOpNode2 = this._graph.addNode({
      shape: 'operation-node',
      width: GraphConstant.nodeWidth,
      height: 40,
    });

    const newNode1Des: NodeInfo = { type: NodeType.Info, current: newNode1, prev: currentOpNode, next: [newOpNode1] };
    const newNode2Des: NodeInfo = { type: NodeType.Info, current: newNode2, prev: currentOpNode, next: [newOpNode2] };
    const newOpdes1: NodeInfo = { type: NodeType.AddApproveNode, current: newOpNode1, prev: newNode1, next: opNodeData.next };
    const newOpdes2: NodeInfo = { type: NodeType.AddApproveNode, current: newOpNode2, prev: newNode2, next: opNodeData.next };

    newNode1.setData(newNode1Des);
    newNode2.setData(newNode2Des);
    newOpNode1.setData(newOpdes1);
    newOpNode2.setData(newOpdes2);

    opNodeData.next = [newNode];
    currentOpNode.setData(opNodeData);

    this.connect(currentOpNode, newNode);
    this.connectMult(newNode, [newNode1, newNode2]);

    this.connectOpNode(newNode1, newOpNode1);
    this.connectOpNode(newNode2, newOpNode2);

    if (newOpdes1.next) {
      this._graph.addEdge({ source: newOpNode1.id, target: newOpdes1.next[0].id });
      var newPos = newOpNode1.getPosition();
      var currPos = currentOpNode.getPosition();
      const pos = { ...currPos };
      pos.y = newPos.y;
      pos.y += GraphConstant.ySpace*4;

      newOpdes1.next[0].setPosition(pos);
      this.ReSetPostion(newOpdes1.next[0]);
    }
    if (newOpdes2.next) {
      this._graph.addEdge({ source: newOpNode2.id, target: newOpdes2.next[0].id });
      var newPos = newOpNode1.getPosition();
      var currPos = currentOpNode.getPosition();
      const pos = { ...currPos };
      pos.y = newPos.y;
      pos.y += GraphConstant.ySpace*4;

      newOpdes2.next[0].setPosition(pos);
      this.ReSetPostion(newOpdes2.next[0]);
    }
    this.drawerShow = false;
  }
}
