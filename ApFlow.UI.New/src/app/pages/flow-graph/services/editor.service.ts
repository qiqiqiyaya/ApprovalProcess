import { Injectable } from '@angular/core';
import { Graph } from '@antv/x6';
import { FlowGraph } from '../../models/flow-graph';
import { DagreLayout } from '@antv/layout';

@Injectable()
export class EditorService {
  constructor() { }

  private graph: Graph;
  public setGraph(graph: Graph): void {
    this.graph = graph;
  }
  public getGraph(): Graph {
    return this.graph;
  }

  private flowGraph: FlowGraph;
  public setFlowGraph(flowGraph: FlowGraph): void {
    this.flowGraph = flowGraph;
  }
  public getFlowGraph(): FlowGraph {
    return this.flowGraph;
  }

  public refreshGraph(): void {
    // 1. 使用 DagreLayout 计算节点位置
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      ranksep: 15,
      nodesep: 35,
    });
    const layoutedData = dagreLayout.layout(this.flowGraph);
    // 3. 使用计算好的数据渲染图
    this.graph.fromJSON(layoutedData);
    this.graph.centerContent();
  }
}
