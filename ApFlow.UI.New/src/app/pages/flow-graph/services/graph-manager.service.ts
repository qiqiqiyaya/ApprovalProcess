import { EventEmitter, Injectable } from '@angular/core';
import { DagreLayout } from '@antv/layout';
import { BranchManager } from '../models/branch-group-manager';
import { FlowNodeHelper } from '../helper/flow-node-helper';
import { FlowEdgeHelper } from '../helper/flow-edge-helper';
import { NodeShape } from '../components/nodes/node-register';
import { Graph } from '@antv/x6';
import { IBranchGroup, IFlowGraph, IFlowNode } from '../models/graph-definition';
import { BranchGroupHelper } from '../helper/branch-group-helper';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class GraphManagerService {
  /** 是否已经订阅了事件 */
  private eventSubscribed = false;


  private _flowGraph: IFlowGraph;
  public setflowGraph(flowGraph: IFlowGraph): void {
    this._flowGraph = flowGraph;
    this.branchManager = new BranchManager(this);
  }
  public get flowGraph(): IFlowGraph {
    return this._flowGraph;
  }

  private graph: Graph;
  public setGraph(graph: Graph): void {
    this.graph = graph;
  }
  public getGraph(): Graph {
    return this.graph;
  }

  private get groups(): Map<string, IBranchGroup> {
    return this.branchManager.groups;
  }
  /** 分支组管理器 */
  private branchManager: BranchManager;

  private _currentNode: IFlowNode;
  public get currentNode(): IFlowNode {
    return this._currentNode;
  }


  private _currentBranchGroup = new EventEmitter<IBranchGroup>();
  public get $currentBranchGroup(): Observable<IBranchGroup> {
    return this._currentBranchGroup;
  }

  constructor() {
  }

  public render(): void {
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
      this._currentNode = node;
    });
     this.graph.on('node:mousemove', ({ node }) => {
      (this._currentNode as any) = undefined;
    });
    this.eventSubscribed = true;
  }




  /**
   * 创建一个新的节点并添加到图中
   * @param shape 节点形状
   * @returns 新的节点实例
   */
  newNode(shape: string) {
    return FlowNodeHelper.createTo(shape, this._flowGraph);
  }

  /**
   * 创建一个新的边并添加到图中
   * @param sourceId 源节点ID
   * @param targetId 目标节点ID
   * @returns 新的边实例
   */
  newEdge(sourceId: string, targetId: string) {
    return FlowEdgeHelper.createTo(sourceId, targetId, this._flowGraph);
  }


  getNode(id: string) {
    var node = this.findNodeById(id);
    if (node == null)throw new Error(`Node with id ${id} not found`);
    return node;
  }

  /**
   * 根据节点ID查找节点
   * @param id 节点ID
   * @returns 找到的节点或null
   */
  findNodeById(id: string): IFlowNode | null {
    return this._flowGraph.nodes.find(node => node.id === id) || null;
  }

  /**
   * 在指定节点后添加审批节点和操作节点
   * 操作流程：operationNode -> approveNode -> newOperationNode -> nextNode
   * @param operationNode 要添加审批的操作节点
   */
  addApproveNode(operationNode: IFlowNode): void {
    FlowNodeHelper.addApproveNode(operationNode, this._flowGraph);
  }

  /**
   * 创建一个新的分支组
   * @param startNodeId 分支开始节点ID
   */
  addBranch(operationNode: IFlowNode): void {
    BranchGroupHelper.create(operationNode, this._flowGraph, this.branchManager, this._currentBranchGroup);
  }


  /**
   * 创建一个新的流程图，包含开始节点、操作节点和结束节点，以及它们之间的边
   * @returns 新的流程图实例
   */
  newFlow() {
    const stratNode = FlowNodeHelper.createRect('开始');
    const operationNode = FlowNodeHelper.create(NodeShape.operation);
    const endNode = FlowNodeHelper.createRect('结束');

    const group: IFlowGraph = {
      nodes: [stratNode, operationNode, endNode],
      edges: [
        FlowEdgeHelper.create(stratNode.id, operationNode.id),
        FlowEdgeHelper.create(operationNode.id, endNode.id)
      ]
    }
    return group;
  }
}
