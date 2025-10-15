import { inject, Injectable } from '@angular/core';
import { GraphConstant } from '../graph-constant';
import { NodeInfo, NodeType } from '../node-description';
import { Node as XNode } from '@antv/x6';
import { X6FlowGraph } from './x6-flow-graph';
import { BaseService } from './base-service';

@Injectable()
export class ParallelApproveNodeService extends BaseService {

  constructor() {
    super(inject(X6FlowGraph));
  }

  add() {
    /**
     *       prev 
     *         |
     *      current - 当前的操作节点
     *         |
     *       next
     */
    /****** 转换为 ******/
    /**
     *       prev
     *         |
     *      current - 当前的操作节点
     *         |
     *   添加并行审批按钮
     *      |     |
     *  操作节点  操作节点
     *      |     |
     * 审批人节点 审批人节点
     *      |     |
     *  操作节点  操作节点
     *      |     |
     *      合并节点
     *         |
     *      操作节点
     *         |
     *       next
     */

    this._flowGraph.removeAllOutgoingEdges(this.currentNode);

    const cuInfo = this.currentNodeInfo;
    /* 添加并行审批按钮 */
    const paNode = this.addParallelApproveNode();
    const paInfo: NodeInfo = { type: NodeType.ParallelApproveNode, current: paNode, prevs: [this.currentNode] };
    paNode.setData(paInfo);

    /* 合并节点 */
    const mergeNode = this.addParallelApproveMerge();
    const mergeNodeInfo: NodeInfo = { type: NodeType.Info, current: mergeNode };
    mergeNode.setData(mergeNodeInfo);

    /* 初始只加 2 条链 */
    this.addApprovalChain(paNode, mergeNode);
    this.addApprovalChain(paNode, mergeNode);

    mergeNodeInfo.next = cuInfo.next;
    mergeNode.setData(mergeNodeInfo);
    cuInfo.next=[paNode];
    this.currentNode.setData(cuInfo);

    this._flowGraph.rePositionForNext(this.currentNode);
    this._flowGraph.autoConnect(this.currentNode);

    this._flowGraph.closeAddNodeModal();
  }

  /**
   * 添加一条审批链
   */
  private addApprovalChain(parallelApprovalNode:XNode,mergeNode:XNode) {
    /**
     *       prev
     *         |
     *   添加并行审批按钮   -----  parallelApprovalNode
     *        |         | 开始
     *   操作节点 1      | 
     *        |         | 
     *  审批人节点        | 审批链  
     *        |         | 
     *   操作节点  2     | 
     *        |         | 结束
     *      合并节点    -----    mergeNode
     *         |
     *      操作节点
     *         |
     *       next
     */

    /* 操作节点 1 */
    const opNode1= this._flowGraph.addOperationNode();
    const opNode1Info: NodeInfo = { type: NodeType.OperationNode, current: opNode1, prevs: [parallelApprovalNode] };

    /* 审批人节点 */
    const apNode= this._flowGraph.addApprovalNode();
    const apNodeInfo: NodeInfo = { type: NodeType.OperationNode, current: apNode, prevs: [opNode1] };

    /* 操作节点 2 */
    const opNode2 = this._flowGraph.addOperationNode();
    const opNode2Info: NodeInfo = { type: NodeType.OperationNode, current: opNode2, prevs: [apNode] };

    const plaNodeInfo = parallelApprovalNode.getNodeInfo();
    if (!plaNodeInfo.next) plaNodeInfo.next = [];

    plaNodeInfo.next.push(opNode1);
    opNode1Info.next=[apNode];
    apNodeInfo.next=[opNode2];
    opNode2Info.next=[mergeNode];

    opNode1.setData(opNode1Info);
    apNode.setData(apNodeInfo);
    opNode2.setData(opNode2Info);

    const mergeInfo = mergeNode.getNodeInfo();
    if (!mergeInfo.prevs) mergeInfo.prevs = [];
    mergeInfo.prevs.push(opNode2);
  }

  /**
   * 添加并行审批节点
   * @returns 
   */
  private addParallelApproveNode() {
    return this.graph.addNode({
      shape: 'parallel-approval-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight
    });
  }

  /**
   * 并行审批合并节点
   * @returns 
   */
  private addParallelApproveMerge() {
    return this.graph.addNode({
      shape: 'parallel-approval-Merge-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "合并审批"
    });
  }

}
