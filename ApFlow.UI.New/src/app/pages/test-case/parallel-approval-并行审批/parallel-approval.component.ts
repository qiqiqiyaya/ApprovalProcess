import { Component, OnInit } from '@angular/core';
import { NodeShape } from '../../flow-graph/components/nodes/node-register';
import { FlowEdgeHelper } from '../../flow-graph/helper/flow-edge-helper';
import { FlowNodeHelper } from '../../flow-graph/helper/flow-node-helper';
import { IFlowGraph } from '../../flow-graph/models/graph-definition';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-parallel-approval',
  templateUrl: './parallel-approval.component.html',
  styleUrls: ['./parallel-approval.component.css'],
  standalone: false
})
export class ParallelApprovalComponent implements OnInit {

  flowGraph: IFlowGraph;
  constructor(private message: NzMessageService) { }

  ngOnInit() {
    this.flowGraph = this.newFlow();
    this.checkGraphNodes();
  }

  /**
   * 创建一个新的并行审批流程图
   * @returns 新的流程图实例
   */
  newFlow() {
    const startNode = FlowNodeHelper.create(NodeShape.start);
    const operationNode = FlowNodeHelper.create(NodeShape.operation);
    const parallelApprovalNode = FlowNodeHelper.create(NodeShape.parallelApproval);
    const approveNode1 = FlowNodeHelper.create(NodeShape.approve);
    const approveNode2 = FlowNodeHelper.create(NodeShape.approve);
    const parallelApprovalMergeNode = FlowNodeHelper.create(NodeShape.parallelApprovalMerge);
    const endNode = FlowNodeHelper.create(NodeShape.end);

    const group: IFlowGraph = {
      nodes: [startNode, operationNode, parallelApprovalNode, approveNode1, approveNode2, parallelApprovalMergeNode, endNode],
      edges: [
        FlowEdgeHelper.create(startNode.id, operationNode.id),
        FlowEdgeHelper.create(operationNode.id, parallelApprovalNode.id),
        FlowEdgeHelper.create(parallelApprovalNode.id, approveNode1.id),
        FlowEdgeHelper.create(parallelApprovalNode.id, approveNode2.id),
        FlowEdgeHelper.create(approveNode1.id, parallelApprovalMergeNode.id),
        FlowEdgeHelper.create(approveNode2.id, parallelApprovalMergeNode.id),
        FlowEdgeHelper.create(parallelApprovalMergeNode.id, endNode.id)
      ]
    }
    return group;
  }

  /**
   * 检查图形节点是否满足并行审批流程需求
   */
  checkGraphNodes() {
    const hasParallelApprovalNode = this.flowGraph.nodes.some(node => node.shape === NodeShape.parallelApproval);
    const hasParallelApprovalMergeNode = this.flowGraph.nodes.some(node => node.shape === NodeShape.parallelApprovalMerge);

    if (!hasParallelApprovalNode || !hasParallelApprovalMergeNode) {
      this.message.warning('检测到现有图形节点无法满足并行审批流程需求，需要创建新的图形节点');
    }
  }

  /**
   * 触发节点检查机制
   */
  triggerNodeCheck() {
    this.checkGraphNodes();
  }
}
