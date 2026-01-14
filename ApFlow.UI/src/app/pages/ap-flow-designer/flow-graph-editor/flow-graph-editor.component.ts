import { ChangeDetectionStrategy, Component, DestroyRef, inject, Injector, OnDestroy, OnInit } from '@angular/core';
import { Graph, Node } from '@antv/x6';
import { GraphConstant } from '../../models/graph-constant';
import { NodeInfo, NodeType } from '../../models/node-description';
import { X6FlowGraph } from '../services/x6-flow-graph';
import { CustomShapeRegister } from '../custom-shape-register';
import { CustomShapeNames } from '../custom-shape-names';

@Component({
  selector: 'app-flow-graph-editor',
  templateUrl: './flow-graph-editor.component.html',
  styleUrls: ['./flow-graph-editor.component.css'],
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FlowGraphEditorComponent implements OnInit, OnDestroy {
  
  private static readonly CONTAINER_ID = 'container';
  private static readonly START_NODE_ID = 'start';
  private static readonly START_NODE_LABEL = '发起人';
  private static readonly OPERATION_NODE_ID = 'add';
  private static readonly END_NODE_ID = 'end';
  private static readonly END_NODE_LABEL = '结束';

  private graph: Graph | null = null;
  private readonly flowGraph = inject(X6FlowGraph);
  private readonly destroyRef = inject(DestroyRef);

  constructor(injector: Injector) {
    CustomShapeRegister.register(injector);
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.initializeGraph();
    const nodes = this.createInitialNodes();
    this.buildNodeRelationships(nodes);
    this.initializeFlowGraph(nodes);
  }

  ngOnDestroy(): void {
    this.disposeGraph();
  }

  private initializeGraph(): void {
    const container = document.getElementById(FlowGraphEditorComponent.CONTAINER_ID);
    
    if (!container) {
      console.error(`容器元素 #${FlowGraphEditorComponent.CONTAINER_ID} 未找到`);
      return;
    }

    this.graph = new Graph({
      container,
      grid: true,
      panning: true,
      mousewheel: true,
    });
  }

  private createInitialNodes(): InitialNodes {
    if (!this.graph) {
      throw new Error('Graph 实例未初始化');
    }

    const startNode = this.createStartNode();
    const operationNode = this.createOperationNode(startNode);
    const endNode = this.createEndNode(operationNode);

    return { startNode, operationNode, endNode };
  }

  private createStartNode(): Node {
    if (!this.graph) {
      throw new Error('Graph 实例未初始化');
    }

    const startNode = this.graph.addNode({
      id: FlowGraphEditorComponent.START_NODE_ID,
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: FlowGraphEditorComponent.START_NODE_LABEL
    });

    const startInfo: NodeInfo = {
      type: NodeType.Start,
      current: startNode,
      next: []
    };

    startNode.setData(startInfo);

    return startNode;
  }

  private createOperationNode(prevNode: Node): Node {
    if (!this.graph) {
      throw new Error('Graph 实例未初始化');
    }

    const operationNode = this.graph.addNode({
      id: FlowGraphEditorComponent.OPERATION_NODE_ID,
      shape: CustomShapeNames.addNodeBtn,
      width: GraphConstant.nodeWidth,
      height: 40
    });

    const operationNodeInfo: NodeInfo = {
      type: NodeType.OperationNode,
      current: operationNode,
      prevs: [prevNode],
      next: []
    };

    operationNode.setData(operationNodeInfo);

    return operationNode;
  }

  private createEndNode(prevNode: Node): Node {
    if (!this.graph) {
      throw new Error('Graph 实例未初始化');
    }

    const endNode = this.graph.addNode({
      id: FlowGraphEditorComponent.END_NODE_ID,
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: FlowGraphEditorComponent.END_NODE_LABEL
    });

    const endNodeInfo: NodeInfo = {
      type: NodeType.End,
      current: endNode,
      prevs: [prevNode]
    };

    endNode.setData(endNodeInfo);

    return endNode;
  }

  private buildNodeRelationships(nodes: InitialNodes): void {
    const { startNode, operationNode, endNode } = nodes;

    const startInfo = startNode.getData() as NodeInfo;
    const operationNodeInfo = operationNode.getData() as NodeInfo;

    if (startInfo.next) {
      startInfo.next.push(operationNode);
    }

    if (operationNodeInfo.next) {
      operationNodeInfo.next.push(endNode);
    }

    startNode.setData(startInfo);
    operationNode.setData(operationNodeInfo);
  }

  private initializeFlowGraph(nodes: InitialNodes): void {
    if (!this.graph) {
      throw new Error('Graph 实例未初始化');
    }

    const { startNode, endNode } = nodes;

    this.flowGraph.init(this.graph, startNode, endNode);
    this.flowGraph.establishFlowConnections(startNode);
    this.flowGraph.layoutFlowNodes(startNode);
  }

  private disposeGraph(): void {
    if (this.graph) {
      this.graph.dispose();
      this.graph = null;
    }
  }
}

interface InitialNodes {
  startNode: Node;
  operationNode: Node;
  endNode: Node;
}
