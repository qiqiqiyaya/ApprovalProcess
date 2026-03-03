import { Injectable } from '@angular/core';
import { Graph, Node } from '@antv/x6';
import { FlowGraph } from '../models/flow-graph';
import { FlowLayoutEngine } from './flow-layout-engine.service';
import type { ILayoutConfig, ILayoutResult } from '../models/layout.models';
import { BehaviorSubject } from 'rxjs';
import { FlowNode } from '../models/flow-node';
import { BranchGroupManager } from '../models/branch-group-manager';
import { LayoutError, LayoutErrorCode } from '../models/layout.models';

@Injectable()
export class EditorService {
  /** 是否已经订阅了事件 */
  private eventSubscribed = false;

  /** Layout engine instance */
  private readonly layoutEngine = new FlowLayoutEngine();

  constructor() {}

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
      throw new Error(
        'FlowGraph is not initialized. Please call editorService.setflowGraph() first.',
      );
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
    // 1. 使用 FlowLayoutEngine 计算节点位置（固定 50px 垂直间距）
    try {
      const layoutConfig: ILayoutConfig = {
        verticalSpacing: 50,
        horizontalSpacing: 75,
        baseYOffset: 0,
        centerGraph: true,
      };
      debugger;
      const layoutResult: ILayoutResult = this.layoutEngine.layout(this._flowGraph, layoutConfig);

      // 2. 转换布局结果为 X6 兼容格式
      const layoutedData = {
        nodes: Array.from(layoutResult.nodePositions.values()).map((pos: any) => {
          // 从 FlowGraph 中获取原始节点信息以获取 shape 属性
          const originalNode = this._flowGraph.findNodeById(pos.id);
          if (!originalNode) {
            throw new LayoutError(
              `Node ${pos.id} not found in FlowGraph during X6 data conversion`,
              LayoutErrorCode.INVALID_GRAPH
            );
          }

          return {
            id: pos.id,
            x: pos.x,
            y: pos.y,
            width: pos.width,
            height: pos.height,
            // 确保 shape 属性存在，提供默认值以防未定义
            shape: originalNode.shape ?? 'rect',
            // 可选：包含节点的其他属性（如 label, data）
            label: originalNode.label,
            data: originalNode.data,
          };
        }),
        edges: this._flowGraph.edges.map((edge) => ({
          id: `${edge.source}-${edge.target}`,
          source: edge.source,
          target: edge.target,
        })),
      };

      // 3. 使用计算好的数据渲染图
      this.graph.fromJSON(layoutedData);
      this.graph.centerContent();

      // 4. 订阅事件
      this.eventSubscribe();

      console.log('Graph rendered with fixed 50px vertical spacing');
      console.log('Layout dimensions:', layoutResult.totalWidth, 'x', layoutResult.totalHeight);
      console.log('Max level:', layoutResult.maxLevel);
    } catch (error) {
      // Handle layout errors with user-friendly messages
      if (error instanceof LayoutError) {
        console.error(`Layout failed (${error.code}): ${error.message}`);
        // TODO: Show user notification (e.g., using nz-message service)
      } else {
        console.error('Unexpected error during layout:', error);
      }
      throw error;
    }
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
