import { inject, Injectable } from '@angular/core';
import { NodeInfo, NodeType } from '../../models/node-description';
import { X6FlowGraph } from './x6-flow-graph';
import { BaseService } from './base-service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { ApprovalSettingsComponent } from '../approval-settings/approval-settings.component';

@Injectable()
export class ApproveNodeService extends BaseService {
  drawer = inject(NzDrawerService);
  constructor() {
    super(inject(X6FlowGraph));
  }

  /**
   * 添加审批节点
   */
  add() {
    /**
     *      current 当前的操作节点
     *         |
     *      newNode - 审批节点
     *         |
     *     newOpNode 操作节点
     */

    this._flowGraph.removeAllNextEdge(this.currentNode);
    const newNode= this._flowGraph.addApprovalNode()
 
    const newNodeInfo: NodeInfo = { type: NodeType.Info, current: newNode, prevs: [this.currentNode] };

    const newOpNode = this._flowGraph.addOperationNode();
    const newOpInfo: NodeInfo = { type: NodeType.OperationNode, current: newOpNode, prevs: [newNode] };

    const cuNext = this.currentNodeInfo.next;
    this.currentNodeInfo.next = [newNode];
    newNodeInfo.next = [newOpNode];
    if (cuNext) newOpInfo.next = cuNext;
    
    newNode.setData(newNodeInfo);
    newOpNode.setData(newOpInfo);

    this._flowGraph.rePositionForNext(this.currentNode);
    this._flowGraph.autoConnect(this.currentNode);

    this._flowGraph.closeAddNodeModal();
  }

  remove() {
     /**
     *       Prev 节点
     *         |
     *      current - 审批节点
     *         |  
     *     addOpNode 操作节点
     *         |              
     *        next  节点
     */
    /****** 转为如下 ******/
     /**
     *       Prev  节点
     *         |              
     *       next  节点
     */
    const info = this.currentNodeInfo;
    if (!info.next) return;

    const prevInfo = info.prevs![0].getNodeInfo();
    this.graph.removeNode(info.current);
    this.graph.removeNode(info.next[0]);

    prevInfo.next = info.next[0].getNodeInfo().next;
    this._flowGraph.rePositionForNext(info.prevs![0]);
    this._flowGraph.autoConnect(info.prevs![0]);
  }

  setApprove(){
      this.drawer.create({
        nzMaskClosable:false,
        nzTitle: '设置审批人',
        nzWidth: 800,
        nzContent: ApprovalSettingsComponent,
      })
  }
}
