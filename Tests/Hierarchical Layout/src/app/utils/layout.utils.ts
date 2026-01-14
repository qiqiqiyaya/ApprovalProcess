// ParallelVerticalLayout 类中修复后的关键方法

import { Edge, Graph,Node } from "@antv/x6";
import { BranchInfo } from "../models/flow-node.model";
import { number } from "@antv/x6/lib/common/animation/interp";


  // 配置参数
export type config = {
    nodeWidth: number,
    nodeHeight: number,
    horizontalGap: number,
    verticalGap: number,
    centerX: number,
  };


export class ParallelVerticalLayout {
  private positions = new Map<string, { x: number, y: number, depth: number }>();
  private columns = new Map<string, number>();
  private branchInfo = new Map<string, BranchInfo>();
  private maxDepth = 0;
  
  // 添加邻接表存储
  private adjacencyList = new Map<string, string[]>();
  private reverseAdjacencyList = new Map<string, string[]>();
  
  constructor(private config: config) {
    this.config = config;
  }

  /**
   * 执行布局（修复版本）
   */
  layout(graph: Graph): void {
    const nodes = graph.getNodes();
    const edges = graph.getEdges();
    
    // 1. 重置状态
    this.reset();
    
    // 2. 构建邻接表
    this.buildAdjacencyLists(edges);
    
    // 3. 找到所有起始节点
    const startNodes = this.findStartNodes(nodes);
    if (startNodes.length === 0) return;
    
    // 4. 分析图结构（修复：同时记录深度和计算位置）
    this.analyzeGraphStructure(startNodes, nodes);
    
    // 5. 分配位置（修复：确保计算所有节点的x,y）
    this.assignPositions();
    
    // 6. 应用布局到图
    this.applyToGraph(graph);
    
    console.log(this.positions);
    const fds=JSON.stringify(Object.fromEntries(this.positions));
    console.log(fds);

    // 7. 调整边
    // this.adjustEdges(graph);
  }

  /**
   * 重置状态
   */
  private reset(): void {
    this.positions.clear();
    this.columns.clear();
    this.branchInfo.clear();
    this.adjacencyList.clear();
    this.reverseAdjacencyList.clear();
    this.maxDepth = 0;
  }

  /**
   * 构建邻接表
   */
  private buildAdjacencyLists(edges: Edge[]): void {
    edges.forEach(edge => {
      const source = edge.getSourceCellId();
      const target = edge.getTargetCellId();
      
      if (source && target) {
        // 正向邻接表
        if (!this.adjacencyList.has(source)) {
          this.adjacencyList.set(source, []);
        }
        this.adjacencyList.get(source)!.push(target);
        
        // 反向邻接表
        if (!this.reverseAdjacencyList.has(target)) {
          this.reverseAdjacencyList.set(target, []);
        }
        this.reverseAdjacencyList.get(target)!.push(source);
      }
    });
  }

  /**
   * 找到所有起始节点（没有入边的节点）
   */
  private findStartNodes(nodes: Node[]): Node[] {
    const startNodes: Node[] = [];
    
    nodes.forEach(node => {
      const incoming = this.reverseAdjacencyList.get(node.id) || [];
      if (incoming.length === 0) {
        startNodes.push(node);
      }
    });
    
    return startNodes;
  }

  /**
   * 分析图结构并计算深度（修复版本）
   */
  private analyzeGraphStructure(startNodes: Node[], allNodes: Node[]): void {
    const visited = new Set<string>();
    
    // 从每个起始节点开始遍历
    startNodes.forEach((startNode, startIndex) => {
      const stack: Array<{ nodeId: string, depth: number }> = [
        { nodeId: startNode.id, depth: 0 }
      ];
      
      while (stack.length > 0) {
        const { nodeId, depth } = stack.pop()!;
        
        if (visited.has(nodeId)) continue;
        visited.add(nodeId);
        
        // 记录深度和初始位置（这里只设置深度，位置后面计算）
        this.positions.set(nodeId, { x: 0, y: 0, depth });
        this.maxDepth = Math.max(this.maxDepth, depth);
        
        // 检查节点类型
        const children = this.adjacencyList.get(nodeId) || [];
        const incoming = this.reverseAdjacencyList.get(nodeId) || [];
        
        // 并行节点（多个出边）
        if (children.length > 1) {
          this.branchInfo.set(nodeId, {
            type: 'parallel',
            branches: children.length,
            startNode: nodeId,
            depth
          });
          
          // 为分支分配初始列
          children.forEach((childId, index) => {
            this.columns.set(childId, index - Math.floor(children.length / 2));
          });
        }
        
        // 合并节点（多个入边）
        if (incoming.length > 1) {
          this.branchInfo.set(nodeId, {
            type: 'merge',
            branches: incoming.length,
            depth
          });
        }
        
        // 继续遍历子节点
        children.forEach(childId => {
          stack.push({ nodeId: childId, depth: depth + 1 });
        });
      }
    });
    
    // 确保所有节点都被处理（处理孤立的节点）
    allNodes.forEach(node => {
      if (!visited.has(node.id)) {
        this.positions.set(node.id, { x: 0, y: 0, depth: 0 });
      }
    });
  }

  /**
   * 分配位置（修复版本）- 计算每个节点的x,y坐标
   */
  private assignPositions(): void {
    debugger;
    const visited = new Set<string>();
    const nodesByDepth = new Map<number, string[]>();
    
    // 按深度分组
    this.positions.forEach((pos, nodeId) => {
      const depth = pos.depth;
      if (!nodesByDepth.has(depth)) {
        nodesByDepth.set(depth, []);
      }
      nodesByDepth.get(depth)!.push(nodeId);
    });
    
    // 为每个深度的节点分配x坐标
    for (let depth = 0; depth <= this.maxDepth; depth++) {
      const nodesAtDepth = nodesByDepth.get(depth) || [];
      
      if (nodesAtDepth.length === 0) continue;
      
      // 对每个深度的节点进行排序
      const sortedNodes = this.sortNodesAtDepth(nodesAtDepth, depth);
      
      // 分配x坐标
      sortedNodes.forEach((nodeId, index) => {
        const pos = this.positions.get(nodeId);
        if (!pos) return;
        
        const branchInfo = this.branchInfo.get(nodeId);
        
        // 计算x坐标
        let x: number;
        
        if (branchInfo) {
          if (branchInfo.type === 'parallel') {
            // 并行节点居中
            x = this.config.centerX;
          } else if (branchInfo.type === 'merge') {
            // 合并节点：找到对应的并行节点并对齐
            const parallelNodeId = this.findParallelNodeForMerge(nodeId);
            if (parallelNodeId) {
              const parallelPos = this.positions.get(parallelNodeId);
              x = parallelPos ? parallelPos.x : this.config.centerX;
            } else {
              x = this.config.centerX;
            }
          } else {
            // 其他有分支信息的节点
            const column = this.columns.get(nodeId) || 0;
            x = this.config.centerX + column * this.config.horizontalGap;
          }
        } else {
          // 普通节点
          const column = this.columns.get(nodeId) || 0;
          
          // 如果有父节点，尝试继承父节点的x坐标
          const parentNodeId = this.findParentNode(nodeId);
          if (parentNodeId) {
            const parentPos = this.positions.get(parentNodeId);
            if (parentPos) {
              x = parentPos.x + column * this.config.horizontalGap;
            } else {
              x = this.config.centerX + column * this.config.horizontalGap;
            }
          } else {
            x = this.config.centerX + column * this.config.horizontalGap;
          }
        }
        
        // 计算y坐标
        const y = depth * this.config.verticalGap;
        
        // 更新位置
        this.positions.set(nodeId, { ...pos, x, y });
        visited.add(nodeId);
      });
    }
    
    // 特殊处理：调整合并节点位置使其与并行节点对齐
    this.adjustMergeNodesPositions();
  }

  /**
   * 按深度对节点排序（处理分支顺序）
   */
  private sortNodesAtDepth(nodeIds: string[], depth: number): string[] {
    // 按列索引排序
    return nodeIds.sort((a, b) => {
      const colA = this.columns.get(a) || 0;
      const colB = this.columns.get(b) || 0;
      return colA - colB;
    });
  }

  /**
   * 查找父节点
   */
  private findParentNode(nodeId: string): string | null {
    const incoming = this.reverseAdjacencyList.get(nodeId) || [];
    return incoming.length > 0 ? incoming[0] : null;
  }

  /**
   * 查找与合并节点对应的并行节点
   */
  private findParallelNodeForMerge(mergeNodeId: string): string | null {
    const mergeDepth = this.positions.get(mergeNodeId)?.depth || 0;
    
    // 向上查找深度差为2的并行节点
    for (const [nodeId, pos] of this.positions) {
      const info = this.branchInfo.get(nodeId);
      if (info && info.type === 'parallel' && 
          Math.abs(pos.depth - mergeDepth) === 2) {
        return nodeId;
      }
    }
    
    return null;
  }

  /**
   * 调整合并节点位置
   */
  private adjustMergeNodesPositions(): void {
    // 找到所有合并节点
    const mergeNodes = Array.from(this.branchInfo.entries())
      .filter(([_, info]) => info.type === 'merge')
      .map(([nodeId]) => nodeId);
    
    mergeNodes.forEach(mergeNodeId => {
      const parallelNodeId = this.findParallelNodeForMerge(mergeNodeId);
      if (parallelNodeId) {
        const parallelPos = this.positions.get(parallelNodeId);
        const mergePos = this.positions.get(mergeNodeId);
        
        if (parallelPos && mergePos) {
          // 将合并节点对齐到并行节点的x坐标
          this.positions.set(mergeNodeId, {
            ...mergePos,
            x: parallelPos.x
          });
        }
      }
    });
  }

  /**
   * 应用布局到图
   */
  private applyToGraph(graph: Graph): void {
    // 先检查所有节点是否有有效的位置
    const missingPositions: string[] = [];
    
    this.positions.forEach((pos, nodeId) => {
      if (pos.x === 0 && pos.y === 0 && nodeId.includes('node_')) {
        missingPositions.push(nodeId);
      }
    });
    
    if (missingPositions.length > 0) {
      console.warn('以下节点位置未正确计算:', missingPositions);
    }
    
    // 应用位置
    this.positions.forEach((pos, nodeId) => {
      const node = graph.getCellById(nodeId);
      if (node && node.isNode()) {
        // 确保位置有效
        const finalX = pos.x !== 0 ? pos.x : 100 + Math.random() * 200;
        const finalY = pos.y !== 0 ? pos.y : 100 + Math.random() * 200;
        
        node.position(finalX, finalY);
      }
    });
  }

  /**
   * 调整边
   */
  private adjustEdges(graph: Graph): void {
    const edges = graph.getEdges();
    
    edges.forEach(edge => {
      const sourceNode = edge.getSourceNode();
      const targetNode = edge.getTargetNode();
      
      if (sourceNode && targetNode) {
        const sourcePos = sourceNode.position();
        const targetPos = targetNode.position();
        
        // 如果节点在同一垂直线上，使用直线
        const isVerticalAlign = Math.abs(sourcePos.x - targetPos.x) < 10;
        
        if (isVerticalAlign) {
          // 垂直连接，使用直线
          edge.setVertices([]);
        } else {
          // 水平偏移连接，使用直角线
          const midX = (sourcePos.x + targetPos.x) / 2;
          edge.setVertices([
            { x: midX, y: sourcePos.y + this.config.nodeHeight / 2 },
            { x: midX, y: targetPos.y - this.config.nodeHeight / 2 }
          ]);
        }
      }
    });
  }

  /**
   * 获取布局信息（用于调试）
   */
  getLayoutInfo(): any {
    const positions: any = {};
    this.positions.forEach((pos, nodeId) => {
      positions[nodeId] = pos;
    });
    
    return {
      positions,
      branchInfo: Object.fromEntries(this.branchInfo),
      columns: Object.fromEntries(this.columns),
      maxDepth: this.maxDepth,
      adjacencyList: Object.fromEntries(this.adjacencyList)
    };
  }
}