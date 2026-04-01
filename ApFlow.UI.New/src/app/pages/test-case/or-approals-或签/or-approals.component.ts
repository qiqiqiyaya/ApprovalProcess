import { Component, OnInit } from '@angular/core';
import { NodeShape } from '../../flow-graph/components/nodes/node-register';
import { FlowEdgeHelper } from '../../flow-graph/helper/flow-edge-helper';
import { FlowNodeHelper } from '../../flow-graph/helper/flow-node-helper';
import { IFlowGraph } from '../../flow-graph/models/graph-definition';

@Component({
  selector: 'app-or-approals',
  templateUrl: './or-approals.component.html',
  styleUrls: ['./or-approals.component.css'],
  standalone: false
})
export class OrApproalsComponent implements OnInit {

  flowGraph: IFlowGraph;
  constructor() { }

  ngOnInit() {
    this.flowGraph = this.newFlow();
  }

  /**
   * 创建或签审批流程图，包含两个分支和或签合并节点
   * @returns 新的流程图实例
   */
  newFlow() {
    const startNode = FlowNodeHelper.create(NodeShape.start);
    const operationNode = FlowNodeHelper.create(NodeShape.operation);
    const orNode = FlowNodeHelper.create(NodeShape.or);

    // 分支1
    const branch1Before = FlowNodeHelper.create(NodeShape.operation);
    const branch1Node = FlowNodeHelper.create(NodeShape.approve);
    const branch1After = FlowNodeHelper.create(NodeShape.operation);

    // 分支2
    const branch2Before = FlowNodeHelper.create(NodeShape.operation);
    const branch2Node = FlowNodeHelper.create(NodeShape.approve);
    const branch2After = FlowNodeHelper.create(NodeShape.operation);

    const orMergeNode = FlowNodeHelper.create(NodeShape.orMerge);
    const operationNode1 = FlowNodeHelper.create(NodeShape.operation);
    const endNode = FlowNodeHelper.create(NodeShape.end);

    const group: IFlowGraph = {
      nodes: [startNode, operationNode, orNode, branch1Before, branch1Node, branch1After, branch2Before, branch2Node, branch2After, orMergeNode, operationNode1, endNode],
      edges: [
        FlowEdgeHelper.create(startNode.id, operationNode.id),
        FlowEdgeHelper.create(operationNode.id, orNode.id),

        // 分支1
        FlowEdgeHelper.create(orNode.id, branch1Before.id),
        FlowEdgeHelper.create(branch1Before.id, branch1Node.id),
        FlowEdgeHelper.create(branch1Node.id, branch1After.id),
        FlowEdgeHelper.create(branch1After.id, orMergeNode.id),

        // 分支2
        FlowEdgeHelper.create(orNode.id, branch2Before.id),
        FlowEdgeHelper.create(branch2Before.id, branch2Node.id),
        FlowEdgeHelper.create(branch2Node.id, branch2After.id),
        FlowEdgeHelper.create(branch2After.id, orMergeNode.id),

        FlowEdgeHelper.create(orMergeNode.id, operationNode1.id),
        FlowEdgeHelper.create(operationNode1.id, endNode.id)
      ]
    }
    return group;
  }
}
