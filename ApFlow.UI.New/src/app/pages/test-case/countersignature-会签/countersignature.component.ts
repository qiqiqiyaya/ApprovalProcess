import { Component, OnInit } from '@angular/core';
import { NodeShape } from '../../flow-graph/components/nodes/node-register';
import { FlowEdgeHelper } from '../../flow-graph/helper/flow-edge-helper';
import { FlowNodeHelper } from '../../flow-graph/helper/flow-node-helper';
import { IFlowGraph } from '../../flow-graph/models/graph-definition';

@Component({
  selector: 'app-countersignature',
  templateUrl: './countersignature.component.html',
  styleUrls: ['./countersignature.component.css'],
  standalone: false
})
export class CountersignatureComponent implements OnInit {

  flowGraph: IFlowGraph;
  constructor() { }

  ngOnInit() {
    this.flowGraph = this.newFlow();
  }

  /**
   * 创建一个新的流程图，包含开始节点、操作节点和结束节点，以及它们之间的边
   * @returns 新的流程图实例
   */
  newFlow() {
    const stratNode = FlowNodeHelper.create(NodeShape.start);
    const operationNode = FlowNodeHelper.create(NodeShape.operation);
    const approveNode = FlowNodeHelper.create(NodeShape.approve);
    const operationNode11 = FlowNodeHelper.create(NodeShape.operation);
    const endNode = FlowNodeHelper.create(NodeShape.end);

    const group: IFlowGraph = {
      nodes: [stratNode, operationNode, approveNode, operationNode11, endNode],
      edges: [
        FlowEdgeHelper.create(stratNode.id, operationNode.id),
        FlowEdgeHelper.create(operationNode.id, approveNode.id),
        FlowEdgeHelper.create(approveNode.id, operationNode11.id),
        FlowEdgeHelper.create(operationNode11.id, endNode.id)
      ]
    }
    return group;
  }
}
