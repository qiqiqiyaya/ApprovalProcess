import { Injectable } from '@angular/core';
import { Graph, Node as XNode } from '@antv/x6';
import { NodeInfo, NodeType } from '../node-description';
import { GraphConstant } from '../graph-constant';


@Injectable()
export class X6FlowGraph {

  private _graph: Graph;
  private _startNode: XNode;
  private _endNode: XNode;
  private _currentOpNode: XNode;

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

  RegisterClickEvent() {
    this.graph.on('node:click', ({ e, x, y, node, view }) => {
      const nodeData = node.getData() as NodeInfo;
      if (nodeData.type == NodeType.AddApproveNode) {
        // this.operation.crrentOp = nodeData.current;
        // this.nodeCreate();
      }
    });
  }

  /**
   * 节点连接 ， 向下连接
   * @param source 
   * @param target 
   */
  connect(source: XNode, target: XNode) {
    this._graph.addEdge({ source: source.id, target: target.id });
  }

  rePositionForNext(XNode: XNode) {
    const data = XNode.getData() as NodeInfo;
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
  private singleNextPositionSet(previous: XNode, current: XNode){
    this.ySet(previous, current);
    this.rePositionForNext(current);
  }

  private multNextPositionSet(previous: XNode, current: XNode[]) {
    const position = previous.getPosition();
    const size = previous.getSize();
    let y = position.y;
    y += size.height;
    y += GraphConstant.ySpace;

    this.xSet(y, current);
    current.forEach(res => {
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

      for (let index = left; index == 0; index--) {
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
      for (let index = right; index == totalCount; index++) {
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
      const mSize= middleNode.getSize();

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
        leftNum +=  Math.abs(elPosition.x) + GraphConstant.xSpace;
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
        rightNum +=  Math.abs(elPosition.x) + GraphConstant.xSpace;
      }
    }
  }
}
