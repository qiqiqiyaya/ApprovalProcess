import { inject, Injectable } from '@angular/core';
import { Edge, Graph, Node as XNode } from '@antv/x6';
import { NodeInfo, NodeType } from '../node-description';
import { GraphConstant } from '../graph-constant';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { AddNodeComponent } from '../add-node/add-node.component';
import { EdgeMap } from '../EdgeMap';


@Injectable()
export class X6FlowGraph {

  private _graph: Graph;
  private _startNode: XNode;
  private _endNode: XNode;
  /* 当前操作的节点 */
  private _currentNode: XNode;
  private modal = inject(NzModalService);
  private _addNodeModal: NzModalRef;

  constructor() { }

  init(graph: Graph, startNode: XNode, endNode: XNode) {
    this._graph = graph;
    this._startNode = startNode;
    this._endNode = endNode;

    this.graph.centerContent();
    this.RegisterClickEvent();
  }

  /** 图 */
  get graph() { return this._graph; }
  /** 起始节点 */
  get startNode() { return this._startNode; }
  /** 终止节点 */
  get endNode() { return this._endNode; }
  /* 当前操作的节点 */
  get currentNode() { return this._currentNode; }

  RegisterClickEvent() {
    this.graph.on('node:click', ({ e, x, y, node, view }) => {

      const nodeData = node.getNodeInfo();
      this._currentNode = node;
      if (nodeData.type == NodeType.OperationNode) {
        this._addNodeModal = this.modal.create({
          nzTitle: "添加",
          nzContent: AddNodeComponent,
          nzFooter: null
        });
      }
    });
  }

  closeAddNodeModal() {
    if (this._addNodeModal) this._addNodeModal.close();
  }

  /**
   * 自动连接节点
   * @param node 
   * @returns 
   */
  autoConnect(node: XNode) {

    const data = node.getData() as NodeInfo;
    const next = data.next;
    if (!next) return;

    /* 所有的连接线 */
    const edges = this._graph.getOutgoingEdges(node);
    let edgeMaps: EdgeMap[] = [];
    if (edges) {
      edgeMaps = EdgeMap.toMaps(edges);
    }

    if (next.length == 1) {
      /* 判断是否已连接过 */
      const targetNode = edgeMaps.find(x => x.target && x.target.id == next[0].id);
      if (targetNode) return;

      this._graph.addEdge({ source: node.id, target: next[0].id });
      this.autoConnect(next[0]);
    }
    else {
      for (let index = 0; index < next.length; index++) {

        const res = next[index];
        /* 判断是否已连接过 */
        const targetNode = edgeMaps.find(x => x.target && x.target.id == res.id);
        if (targetNode) continue;

        this._graph.addEdge({ source: node.id, target: res.id });
        this.autoConnect(res);
      }
    }
  }

  rePositionForNext(node: XNode) {

    const data = node.getData() as NodeInfo;
    const next = data.next;
    if (!next) return;

    if (next.length == 1) {
      this.singleNextPositionSet(data.current, next[0]);
    }

    if (next.length >= 2) {
      this.multNextPositionSet(data.current, next);
    }
  }

  /**
   * 单个节点设置
   * @param previous 
   * @param current 
   */
  private singleNextPositionSet(previous: XNode, current: XNode) {
    this.ySet(previous, current);
    this.rePositionForNext(current);
  }

  private multNextPositionSet(previous: XNode, next: XNode[]) {
    const position = previous.getPosition();
    const size = previous.getSize();
    let y = position.y;
    /* 节点向下添加 */
    y += size.height;
    y += GraphConstant.ySpace;

    this.xSet(y, next);
    next.forEach(res => {
      this.rePositionForNext(res);
    })
  }

  private ySet(referenceNode: XNode, current: XNode) {
    const position = referenceNode.getPosition();
    const size = referenceNode.getSize();
    const pos = { ...position }
    pos.y += size.height;
    pos.y += GraphConstant.ySpace;
    current.setPosition(pos);
  }

  private xSet(y: number, next: XNode[]) {
    const remainder = next.length % 2;
    const totalCount = next.length;

    /* 偶数 */
    if (remainder == 0) {
      /* 左侧节点集 */
      const left = totalCount / 2 - 1;
      let leftNum = GraphConstant.xSpace / 2;

      for (let index = left; index >= 0; index--) {
        const element = next[index];

        const elPosition = { ...element.getPosition() };
        elPosition.x -= leftNum;
        elPosition.y = y;
        element.setPosition(elPosition)

        /* 与左侧邻居的距离 */
        leftNum += Math.abs(elPosition.x) + GraphConstant.xSpace;
      }

      /* 右侧节点集 */
      const right = left + 1;
      let rightNum = GraphConstant.xSpace / 2;
      for (let index = right; index < totalCount; index++) {
        const element = next[index];

        const elPosition = { ...element.getPosition() };
        elPosition.x += rightNum;
        elPosition.y = y;
        element.setPosition(elPosition)

        /* 与左侧邻居的距离 */
        rightNum += Math.abs(elPosition.x) + GraphConstant.xSpace;
      }
    } else {
      /* 奇数 */

      /* 中间节点 */
      const middleIndex = Math.floor(totalCount / 2) + 1;
      const middleNode = next[middleIndex];

      const elPosition = { ...middleNode.getPosition() };
      elPosition.y = y;
      middleNode.setPosition(elPosition);
      const mSize = middleNode.getSize();

      /* 其他平行节点 */

      /* 左侧 */
      const left = Math.floor(totalCount / 2) - 1;
      let leftNum = GraphConstant.xSpace + mSize.width / 2;

      for (let index = left; index == 0; index--) {
        const element = next[index];

        const elPosition = { ...element.getPosition() };
        elPosition.x -= leftNum;
        elPosition.y = y;
        element.setPosition(elPosition)

        /* 与左侧邻居的距离 */
        leftNum += Math.abs(elPosition.x) + GraphConstant.xSpace;
      }

      /* 右侧 */
      const right = Math.ceil(totalCount / 2);
      let rightNum = GraphConstant.xSpace + mSize.width / 2;
      for (let index = right; index == totalCount; index++) {
        const element = next[index];

        const elPosition = { ...element.getPosition() };
        elPosition.x += rightNum;
        elPosition.y = y;
        element.setPosition(elPosition)

        /* 与左侧邻居的距离 */
        rightNum += Math.abs(elPosition.x) + GraphConstant.xSpace;
      }
    }
  }

  /**
   * 移出掉所有连接到 next 的线程
   */
  public removeAllNextEdge(node: XNode) {
    const edges = this._graph.getOutgoingEdges(node);
    /* 所有连接线 */
    let edgeMaps: EdgeMap[] = [];
    if (edges) {
      edgeMaps = EdgeMap.toMaps(edges);
    }
    if (edgeMaps.length == 0) return;

    const nodeInfo = node.getData() as NodeInfo;
    if (!nodeInfo.next) return

    /* 移除连接线 */
    nodeInfo.next.forEach(res => {
      const map = edgeMaps.find(x => x.targetId == res.id);

      if (!map) return;
      this.graph.removeEdge(map.edge);
    });
  }

  /**
   * 移除掉所有外向的连接线
   * @param node 
   */
  public removeAllOutgoingEdges(node: XNode) {
    const edges = this._graph.getOutgoingEdges(node);
    if (!edges) return;
    edges.forEach(res => {
      this.graph.removeEdge(res);
    });
  }

  

  /**
   * 添加操作节点
   */
  public addOperationNode() {
    return this.graph.addNode({ shape: 'operation-node', width: GraphConstant.nodeWidth, height: 40 });
  }

  /**
   * 添加审批人节点
   */
  public addApprovalNode() {
    return this.graph.addNode({
      shape: 'approval-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });
  }


  public getNodeData(){
    const array=[];
    const top= this.startNode.getNodeInfo();

  }
}
