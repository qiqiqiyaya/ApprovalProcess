import { inject, Injectable } from '@angular/core';
import { Edge, Graph, Node as XNode } from '@antv/x6';
import { NodeInfo, NodeType } from '../../models/node-description';
import { GraphConstant } from '../../models/graph-constant';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { EdgeMap } from '../../models/EdgeMap';


@Injectable()
export class X6FlowGraph {

  private _graph: Graph;
  private _startNode: XNode;
  private _endNode: XNode;
  /* 当前操作的节点 */
  private _currentNode: XNode;
  private modal = inject(NzModalService);
  private _addNodeModal: NzModalRef;

  constructor() { }

  init(graph: Graph, startNode: XNode, endNode: XNode) {
    this._graph = graph;
    this._startNode = startNode;
    this._endNode = endNode;

    this.graph.centerContent();
    this.RegisterClickEvent();
  }

  /** 图 */
  get graph() { return this._graph; }
  /** 起始节点 */
  get startNode() { return this._startNode; }
  /** 终止节点 */
  get endNode() { return this._endNode; }
  /* 当前操作的节点 */
  get currentNode() { return this._currentNode; }

  RegisterClickEvent() {
    this.graph.on('node:click', ({ e, x, y, node, view }) => {

      // const nodeData = node.getNodeInfo();
      this._currentNode = node;
      // if (nodeData.type == NodeType.OperationNode) {
      //   this._addNodeModal = this.modal.create({
      //     nzTitle: "添加",
      //     nzContent: AddNodeComponent,
      //     nzFooter: null
      //   });
      // }
    });
  }

  closeAddNodeModal() {
    if (this._addNodeModal) this._addNodeModal.close();
  }

  /**
   * 建立流程连接（有向连接，向下生长）
   * 根据 NodeInfo 的 next 关系自动创建连接线，使用迭代代替递归避免栈溢出
   * @param startNode 起始节点
   */
  establishFlowConnections(startNode: XNode): void {
    console.log('=== 开始重建连接线 ===');
    const nodesToProcess: XNode[] = [startNode];
    const processedNodes = new Set<string>();

    while (nodesToProcess.length > 0) {
      const currentNode = nodesToProcess.shift()!;
      
      if (!currentNode || processedNodes.has(currentNode.id)) {
        continue;
      }

      processedNodes.add(currentNode.id);

      const nodeData = currentNode.getData();
      if (!this.isValidNodeInfo(nodeData)) {
        console.warn('节点数据无效:', currentNode.id);
        continue;
      }

      const nextNodes = nodeData.next;
      console.log(`处理节点 ${currentNode.id}, next 数量: ${nextNodes?.length || 0}`);
      
      if (!nextNodes || nextNodes.length === 0) {
        continue;
      }

      const existingEdges = this._graph.getOutgoingEdges(currentNode);
      const connectedTargetIds = new Set(
        existingEdges?.map(edge => edge.getTargetCellId()) || []
      );
      console.log(`节点 ${currentNode.id} 已存在的连接数: ${existingEdges?.length || 0}`);

      for (const nextNode of nextNodes) {
        console.log(`  检查连接: ${currentNode.id} -> ${nextNode.id}`);
        
        if (connectedTargetIds.has(nextNode.id)) {
          console.log(`  连接已存在，跳过创建但继续处理下游节点`);
        } else {
          console.log(`  创建新连接: ${currentNode.id} -> ${nextNode.id}`);
          this._graph.addEdge({
            source: currentNode.id,
            target: nextNode.id,
            attrs: {
              line: {
                stroke: '#000000',
                strokeWidth: 2,
                targetMarker: {
                  name: 'block',
                  width: 12,
                  height: 8
                }
              }
            },
            router: {
              name: 'normal'
            },
            connector: {
              name: 'normal'
            }
          });
        }

        // 无论连接是否存在，都要将 nextNode 添加到处理队列
        nodesToProcess.push(nextNode);
      }
    }
    console.log('=== 连接线重建完成 ===');
  }

  /**
   * 类型守卫：验证节点数据是否为有效的 NodeInfo
   */
  private isValidNodeInfo(data: unknown): data is NodeInfo {
    return data !== null && 
           typeof data === 'object' && 
           'type' in data && 
           'current' in data;
  }

  /**
   * 布局流程节点（向下生长）
   * 当添加新节点后，自动调整当前节点及其所有下游节点的位置，优化可视化效果
   * 使用迭代代替递归避免栈溢出
   * @param startNode 起始节点
   */
  layoutFlowNodes(startNode: XNode): void {
    const nodesToProcess: XNode[] = [startNode];
    const processedNodes = new Set<string>();

    while (nodesToProcess.length > 0) {
      const currentNode = nodesToProcess.shift()!;
      
      if (!currentNode || processedNodes.has(currentNode.id)) {
        continue;
      }

      processedNodes.add(currentNode.id);

      const nodeData = currentNode.getData();
      if (!this.isValidNodeInfo(nodeData)) {
        continue;
      }

      const nextNodes = nodeData.next;
      if (!nextNodes || nextNodes.length === 0) {
        continue;
      }

      const currentPosition = currentNode.getPosition();
      const currentSize = currentNode.getSize();
      const nextY = currentPosition.y + currentSize.height + GraphConstant.ySpace;

      if (nextNodes.length === 1) {
        this.setNodePosition(nextNodes[0], currentPosition.x, nextY);
        nodesToProcess.push(nextNodes[0]);
      } else {
        this.setMultipleNodesPosition(nextNodes, nextY);
        nextNodes.forEach(nextNode => nodesToProcess.push(nextNode));
      }
    }
  }

  /**
   * 设置节点位置
   */
  private setNodePosition(node: XNode, x: number, y: number): void {
    node.setPosition({ x, y });
  }

  /**
   * 设置多个节点的位置（水平分布）
   */
  private setMultipleNodesPosition(nodes: XNode[], y: number): void {
    const totalCount = nodes.length;
    const isEven = totalCount % 2 === 0;

    if (isEven) {
      this.setEvenNodesPosition(nodes, y);
    } else {
      this.setOddNodesPosition(nodes, y);
    }
  }

  /**
   * 设置偶数个节点的位置
   */
  private setEvenNodesPosition(nodes: XNode[], y: number): void {
    const totalCount = nodes.length;
    const leftCount = totalCount / 2;
    const rightCount = leftCount;

    let leftOffset = GraphConstant.xSpace / 2;
    let rightOffset = GraphConstant.xSpace / 2;

    for (let i = leftCount - 1; i >= 0; i--) {
      const node = nodes[i];
      const currentPosition = node.getPosition();
      const newX = currentPosition.x - leftOffset;
      this.setNodePosition(node, newX, y);
      leftOffset += GraphConstant.xSpace;
    }

    for (let i = leftCount; i < totalCount; i++) {
      const node = nodes[i];
      const currentPosition = node.getPosition();
      const newX = currentPosition.x + rightOffset;
      this.setNodePosition(node, newX, y);
      rightOffset += GraphConstant.xSpace;
    }
  }

  /**
   * 设置奇数个节点的位置
   */
  private setOddNodesPosition(nodes: XNode[], y: number): void {
    const totalCount = nodes.length;
    const middleIndex = Math.floor(totalCount / 2);
    const middleNode = nodes[middleIndex];

    this.setNodePosition(middleNode, middleNode.getPosition().x, y);

    const middleNodeSize = middleNode.getSize();
    let leftOffset = GraphConstant.xSpace + middleNodeSize.width / 2;
    let rightOffset = GraphConstant.xSpace + middleNodeSize.width / 2;

    for (let i = middleIndex - 1; i >= 0; i--) {
      const node = nodes[i];
      const currentPosition = node.getPosition();
      const newX = currentPosition.x - leftOffset;
      this.setNodePosition(node, newX, y);
      leftOffset += GraphConstant.xSpace;
    }

    for (let i = middleIndex + 1; i < totalCount; i++) {
      const node = nodes[i];
      const currentPosition = node.getPosition();
      const newX = currentPosition.x + rightOffset;
      this.setNodePosition(node, newX, y);
      rightOffset += GraphConstant.xSpace;
    }
  }

  /**
   * 移出掉所有连接到 next 的线程
   */
  public removeAllNextEdge(node: XNode) {
    console.log(`=== 删除节点 ${node.id} 的所有连接线 ===`);
    const edges = this._graph.getOutgoingEdges(node);
    /* 所有连接线 */
    let edgeMaps: EdgeMap[] = [];
    if (edges) {
      edgeMaps = EdgeMap.toMaps(edges);
    }
    console.log(`找到 ${edgeMaps.length} 条连接线`);
    if (edgeMaps.length == 0) return;

    const nodeInfo = node.getData() as NodeInfo;
    if (!nodeInfo.next) return

    console.log(`节点 ${node.id} 的 next 数量: ${nodeInfo.next.length}`);

    /* 移除连接线 */
    nodeInfo.next.forEach(res => {
      const map = edgeMaps.find(x => x.targetId == res.id);

      if (!map) return;
      console.log(`  删除连接线: ${node.id} -> ${res.id}`);
      this.graph.removeEdge(map.edge);
    });
    console.log(`=== 连接线删除完成 ===`);
  }

  /**
   * 移除掉所有外向的连接线
   * @param node 
   */
  public removeAllOutgoingEdges(node: XNode) {
    const edges = this._graph.getOutgoingEdges(node);
    if (!edges) return;
    edges.forEach(res => {
      this.graph.removeEdge(res);
    });
  }

  

  /**
   * 添加操作节点
   */
  public addOperationNode() {
    return this.graph.addNode({ shape: 'operation-node', width: GraphConstant.nodeWidth, height: 40 });
  }

  /**
   * 添加审批人节点
   */
  public addApprovalNode() {
    return this.graph.addNode({
      shape: 'approval-node',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: "审批"
    });
  }


  public getNodeData(){
    const array=[];
    // const top= this.startNode.getNodeInfo();

  }
}
