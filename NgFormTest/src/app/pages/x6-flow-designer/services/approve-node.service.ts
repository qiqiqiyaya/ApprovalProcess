import { inject, Injectable } from '@angular/core';
import { X6FlowGraph } from './x6-flow-graph';
import { Graph, Node as XNode } from '@antv/x6';
import { GraphConstant } from '../graph-constant';
import { NodeInfo, NodeType } from '../node-description';

@Injectable()
export class ApproveNodeService {

  private _flowGraph = inject(X6FlowGraph);
  private get graph() {
    return this._flowGraph.graph;
  }

  /* 当前操作的节点 */
  private get currentNode() { return this._flowGraph.currentNode; }
  private get currentNodeInfo(){ return this.currentNode.getData() as NodeInfo }

  constructor() { }

  /**
   * 添加的审批节点
   */
  private _approveNode: XNode;
  get approveNode() {
    return this._approveNode;
  }

  /**
   * 添加审批节点
   */
  addApproveNode() {
    /**
     *      current 当前的操作节点
     *         |
     *      newNode - 审批节点
     *         |
     *     newOpNode 操作节点
     */

    this._flowGraph.removeAllNextEdge(this.currentNode);

    const newNode = this.graph.addNode({
      shape: 'approval-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });
    debugger;
    this._approveNode = newNode;
    const newNodeInfo: NodeInfo = { type: NodeType.Info, current: newNode, prev: this.currentNode };

    const newOpNode = this._flowGraph.addOperationNode();
    const newOpInfo: NodeInfo = { type: NodeType.AddApproveNode, current: newOpNode, prev: newNode };

    const cuNext = this.currentNodeInfo.next;
    this.currentNodeInfo.next = [newNode];
    newNodeInfo.next = [newOpNode];
    if (cuNext) newOpInfo.next = cuNext;
    
    newNode.setData(newNodeInfo);
    newOpNode.setData(newOpInfo);

    this._flowGraph.rePositionForNext(this.currentNode);
    this._flowGraph.autoConnect(this.currentNode);
  }
}
