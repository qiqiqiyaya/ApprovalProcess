import { Injectable } from '@angular/core';
import { Graph, Node } from '@antv/x6';
import { FlowGraph } from '../models/flow-graph';
import { DagreLayout } from '@antv/layout';
import { BehaviorSubject } from 'rxjs';
import { FlowNode } from '../models/flow-node';
import { registerInfo } from '@antv/x6-angular-shape';
import { BranchGroupManager } from '../models/branch-group-manager';

@Injectable()
export class EditorService {
  /** 是否已经订阅了事件 */
  private eventSubscribed = false;

  constructor() { }

  private graph: Graph;
  public setGraph(graph: Graph): void {
    this.graph = graph;
  }
  public getGraph(): Graph {
    return this.graph;
  }

  private _flowGraph: FlowGraph;
  public setflowGraph(flowGraph: FlowGraph): void {
    this._flowGraph = flowGraph;
  }
  public flowGraph(): FlowGraph {
    return this._flowGraph;
  }

  /**
   * Gets the branch group manager from the current flow graph
   * @returns BranchGroupManager instance
   * @throws Error if flow graph is not initialized
   */
  public getBranchGroupManager(): BranchGroupManager {
    if (!this._flowGraph) {
      throw new Error('FlowGraph is not initialized. Please call editorService.setflowGraph() first.');
    }
    return (this._flowGraph as any).branchManager as BranchGroupManager;
  }

  private _currentNode = new BehaviorSubject<Node>(new Node());
  /** 当前选中的节点 */
  public get currentNode$(): BehaviorSubject<Node> {
    return this._currentNode;
  }
  /** 当前选中的节点 */
  public get currentNode(): Node {
    return this._currentNode.value;
  }
  /** 当前选中的节点对应的FlowNode */
  public getFolwNode(): FlowNode {
    const flowNode = this._flowGraph.findNodeById(this.currentNode.id);
    if (!flowNode) throw new Error('当前选中的节点不存在对应的FlowNode');
    return flowNode;
  }

  public renderGraph(): void {
    // 1. 使用 DagreLayout 计算节点位置
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      rankdir: 'TB',
      ranksep: 35,
      nodesep: 75,
    });
    const layoutedData = dagreLayout.layout(this._flowGraph);

    // 4. 关键：修正坐标，让节点中心对齐
    layoutedData.nodes = layoutedData.nodes!.map((node: any) => {
      // 计算中心偏移：x = 初始x - 宽度/2，y = 初始y - 高度/2
      const centerX = node.x - node.width / 2;
      const centerY = node.y - node.height / 2;
      node.x = centerX;
      node.y = centerY;
      return node;
    });

    registerInfo.forEach(() => {
      // Placeholder for node registration logic
    });

    // 2. 使用计算好的数据渲染图
    this.graph.fromJSON(layoutedData);
    this.graph.centerContent();
    // 3. 订阅事件
    this.eventSubscribe();

    console.log(this.graph.toJSON());
  }

  private eventSubscribe() {
    if (this.eventSubscribed) return;
    /* 订阅事件 */
    this.graph.on('node:mouseenter', ({ node }) => {
      this._currentNode.next(node);
    });
    this.eventSubscribed = true;
  }
}
