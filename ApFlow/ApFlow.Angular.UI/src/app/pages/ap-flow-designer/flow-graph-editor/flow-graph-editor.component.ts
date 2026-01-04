import { Component, inject, OnInit } from '@angular/core';
import { Graph } from '@antv/x6';
import { GraphConstant } from '../../x6-flow-designer/graph-constant';
import { NodeInfo, NodeType } from '../../models/node-description';
import { X6FlowGraph } from '../services/x6-flow-graph';
import { CustomShapeNames } from '../../ap-flow-components/custom-shape-names';

@Component({
  selector: 'app-flow-graph-editor',
  templateUrl: './flow-graph-editor.component.html',
  styleUrls: ['./flow-graph-editor.component.css'],
  standalone: false
})
export class FlowGraphEditorComponent implements OnInit {

  flowGraph = inject(X6FlowGraph);

  constructor() { }

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

    const operationNode = graph.addNode({ id: 'add', shape: CustomShapeNames.apNode, width: GraphConstant.nodeWidth, height: 40 });
    const operationNodeInfo: NodeInfo = { type: NodeType.OperationNode, current: operationNode, prevs: [startNode], next: [] };

    const endNode = graph.addNode({ id: 'end', width: GraphConstant.nodeWidth, height: GraphConstant.nodeHeight, label: '结束' });
    const endNodeInfo: NodeInfo = { type: NodeType.End, current: endNode, prevs: [operationNode] };

    startInfo.next?.push(operationNode);
    operationNodeInfo.next?.push(endNode);

    startNode.setData(startInfo);
    operationNode.setData(operationNodeInfo);
    endNode.setData(endNodeInfo);

    this.flowGraph.init(graph, startNode, endNode);
    this.flowGraph.autoConnect(startNode);
    this.flowGraph.rePositionForNext(startNode);
  }
}
 